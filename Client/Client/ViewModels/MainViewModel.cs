using Client.Commands;
using Client.State.Authenticators;
using Client.State.Navigators;
using System.Windows.Input;

namespace Client.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public IAuthenticator Authenticator { get; set; }


    ICommand UpdateCurrentViewModelCommand { get; }

    public MainViewModel(INavigator navigator, IAuthenticator authenticator)
    {
        Navigator = navigator;
        Authenticator = authenticator;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        UpdateCurrentViewModelCommand.Execute(ViewType.Login);
    }
}
