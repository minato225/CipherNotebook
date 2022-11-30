using CipherNoteBook.Domain.Services.CipherService;
using CipherNoteBook.WPF.ViewModels;
using System.Threading.Tasks;

namespace CipherNoteBook.WPF.Commands;

public class DecryptRequestCommand : AsyncCommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly ICypherFileTranspher _cypherFileTranspher;

    public DecryptRequestCommand(HomeViewModel homeViewModel, ICypherFileTranspher cypherFileTranspher)
    {
        _homeViewModel = homeViewModel;
        _cypherFileTranspher = cypherFileTranspher;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _homeViewModel.DecryptedFileText = await _cypherFileTranspher.Decrypt(_homeViewModel.EncryptedFileText, _homeViewModel.SessionKey);
        _homeViewModel.GenRsaBtnIsEnabled = true;
        _homeViewModel.DecryptBtnIsEnabled = false;
        _homeViewModel.EnterFileNamePanelIsEnabled = false;
    }

    public override bool CanExecute(object parameter) => base.CanExecute(parameter);
}