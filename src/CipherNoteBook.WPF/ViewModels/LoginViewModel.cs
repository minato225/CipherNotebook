using CipherNoteBook.WPF.State.Authenticators;
using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.WPF.Commands;
using System.Windows.Input;

namespace CipherNoteBook.WPF.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(CanLogin));
        }
    }

    private string _otpText;
    public string OtpText
    {
        get => _otpText;
        set
        {
            _otpText = value;
            OnPropertyChanged();
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
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
