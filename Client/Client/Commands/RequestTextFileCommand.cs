using Client.WPF.Services.CipherService;
using Client.WPF.ViewModels;
using System.Threading.Tasks;

namespace Client.WPF.Commands;

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

        if (string.IsNullOrEmpty(mes.ErrorMessage))
        {
            _homeViewModel.DecryptBtnIsEnabled = true;
            _homeViewModel.SessionKey = mes.EncryptedSessionKey;
            _homeViewModel.EncryptedFileText = mes?.EncryptedFileText;
            return;
        }
    }

    public override bool CanExecute(object parameter) => base.CanExecute(parameter);
}