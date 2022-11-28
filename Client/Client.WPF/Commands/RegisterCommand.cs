using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Client.WPF.State.Authenticators;
using Client.WPF.ViewModels;
using Client.WPF.State.Navigators;
using Client.WPF.Services.AuthService.interfaces;

namespace Client.WPF.Commands;

public class RegisterCommand : AsyncCommandBase
{
    private readonly RegisterViewModel _registerViewModel;
    private readonly IAuthenticator _authenticator;
    private readonly IRenavigator _registerRenavigator;

    public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator registerRenavigator)
    {
        _registerViewModel = registerViewModel;
        _authenticator = authenticator;
        _registerRenavigator = registerRenavigator;

        _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
    }

    public override bool CanExecute(object parameter) => _registerViewModel.CanRegister && base.CanExecute(parameter);

    public override async Task ExecuteAsync(object parameter)
    {
        _registerViewModel.ErrorMessage = string.Empty;

        try
        {
            var registrationResult = await _authenticator.Register(
                   _registerViewModel.Email,
                   _registerViewModel.Username,
                   _registerViewModel.Password,
                   _registerViewModel.ConfirmPassword);

            if (registrationResult == RegistrationResult.Success)
            {
                _registerRenavigator.Renavigate();
                return;
            }

            _registerViewModel.ErrorMessage = registrationResult switch
            {
                RegistrationResult.PasswordsDoNotMatch => "Password does not match confirm password.",
                RegistrationResult.EmailAlreadyExists => "An account for this email already exists.",
                RegistrationResult.UsernameAlreadyExists => "An account for this username already exists.",
                _ => "Registration failed.",
            };
        }
        catch (Exception)
        {
            _registerViewModel.ErrorMessage = "Registration failed.";
        }
    }

    private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
        {
            OnCanExecuteChanged();
        }
    }
}
