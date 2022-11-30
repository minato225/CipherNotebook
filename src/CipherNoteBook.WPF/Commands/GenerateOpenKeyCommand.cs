using CipherNoteBook.Domain.Services.CipherService;
using CipherNoteBook.WPF.ViewModels;
using System.Threading.Tasks;

namespace CipherNoteBook.WPF.Commands;

public class GenerateOpenKeyCommand : AsyncCommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly ICypherFileTranspher _cypherFileTranspher;

    public GenerateOpenKeyCommand(HomeViewModel homeViewModel, ICypherFileTranspher cypherFileTranspher)
    {
        _homeViewModel = homeViewModel;
        _cypherFileTranspher = cypherFileTranspher;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _homeViewModel.OpenKey = await _cypherFileTranspher.GenOPenKey();
        _homeViewModel.GenRsaBtnIsEnabled = _homeViewModel.OpenKey == "Bad Request";
        _homeViewModel.EnterFileNamePanelIsEnabled = true;
    }

    public override bool CanExecute(object parameter) => base.CanExecute(parameter);
}
