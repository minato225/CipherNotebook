using Client.WPF.Services.CipherService;
using Client.WPF.ViewModels;
using System.Threading.Tasks;

namespace Client.WPF.Commands;

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
        _homeViewModel.GenRsaBtnIsEnabled = false;
        _homeViewModel.EnterFileNamePanelIsEnabled = true;
    }

    public override bool CanExecute(object parameter) => base.CanExecute(parameter);
}
