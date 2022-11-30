using CipherNoteBook.WPF.State.Authenticators;
using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.WPF.Commands;
using System.Windows.Input;

namespace CipherNoteBook.WPF.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    #region Prop
    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(CanRegister));
        }
    }

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(CanRegister));
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
            OnPropertyChanged(nameof(CanRegister));
        }
    }

    private string _confirmPassword;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
            OnPropertyChanged(nameof(CanRegister));
        }
    }
    #endregion

    public bool CanRegister => !string.IsNullOrEmpty(Email) &&
        !string.IsNullOrEmpty(Username) &&
        !string.IsNullOrEmpty(Password) &&
        !string.IsNullOrEmpty(ConfirmPassword);

    public ICommand RegisterCommand { get; }

    public ICommand ViewLoginCommand { get; }

    public MessageViewModel ErrorMessageViewModel { get; }

    public string ErrorMessage
    {
        set => ErrorMessageViewModel.Message = value;
    }

    public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator, IRenavigator loginRenavigator)
    {
        ErrorMessageViewModel = new MessageViewModel();

        RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
        ViewLoginCommand = new RenavigateCommand(loginRenavigator);
    }

    public override void Dispose()
    {
        ErrorMessageViewModel.Dispose();

        base.Dispose();
    }
}
