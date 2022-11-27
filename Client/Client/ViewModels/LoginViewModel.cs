using Client.Commands;
using Client.State.Authenticators;
using Client.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels;

public class LoginViewModel : BaseViewModel
{
	private string _userName;

	public string UserName
	{
		get => _userName;
        set { 
			_userName = value;
			OnPropertyChanged();
		}
	}

    private string _password;
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanLogin));
        }
    }

    public bool CanLogin => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

    public MessageViewModel ErrorMessageViewModel { get; }

    public string ErrorMessage
    {
        set => ErrorMessageViewModel.Message = value;
    }

    public ICommand LoginCommand { get; }
    public ICommand ViewRegisterCommand { get; }

    public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
    {
        ErrorMessageViewModel = new MessageViewModel();

        LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
        ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
    }

    public override void Dispose()
    {
        ErrorMessageViewModel.Dispose();

        base.Dispose();
    }
}
