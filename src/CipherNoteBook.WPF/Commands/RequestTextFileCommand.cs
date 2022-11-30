using CipherNoteBook.Domain.Services.CipherService;
using CipherNoteBook.WPF.ViewModels;
using System.Threading.Tasks;

namespace CipherNoteBook.WPF.Commands;

public class RequestTextFileCommand : AsyncCommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly ICypherFileTranspher _cypherFileTranspher;

    public RequestTextFileCommand(HomeViewModel homeViewModel, ICypherFileTranspher cypherFileTranspher)
    {
        _homeViewModel = homeViewModel;
        _cypherFileTranspher = cypherFileTranspher;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        var filename = _homeViewModel.FileName;
        var mes = await _cypherFileTranspher.RequestFile(filename);

        if (mes is null)
        {
            _homeViewModel.EncryptedFileText = "Null";
            return;
        }

        if (!string.IsNullOrEmpty(mes.ErrorMessage))
        {
            _homeViewModel.EncryptedFileText = mes?.ErrorMessage;
            return;
        }

        _homeViewModel.DecryptBtnIsEnabled = true;
        _homeViewModel.EnterFileNamePanelIsEnabled = false;
        _homeViewModel.SessionKey = mes.EncryptedSessionKey;
        _homeViewModel.EncryptedFileText = mes?.EncryptedFileText;
    }

    public override bool CanExecute(object parameter) => base.CanExecute(parameter);
}