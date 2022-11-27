﻿using Client.Services.AuthService;
using Client.State.Authenticators;
using Client.State.Navigators;
using Client.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System;

namespace Client.Commands;

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

            switch (registrationResult)
            {
                case RegistrationResult.Success:
                    _registerRenavigator.Renavigate();
                    break;
                case RegistrationResult.PasswordsDoNotMatch:
                    _registerViewModel.ErrorMessage = "Password does not match confirm password.";
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    _registerViewModel.ErrorMessage = "An account for this email already exists.";
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    _registerViewModel.ErrorMessage = "An account for this username already exists.";
                    break;
                default:
                    _registerViewModel.ErrorMessage = "Registration failed.";
                    break;
            }
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
