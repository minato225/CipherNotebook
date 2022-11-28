﻿using Client.DataBase.Models;
using Client.WPF.Services.AuthService.interfaces;
using System;
using System.Threading.Tasks;

namespace Client.WPF.State.Authenticators;

public class Authenticator : IAuthenticator
{
    private IAuthenticatorService _authenticatorService;

    public Authenticator(IAuthenticatorService authenticatorService) => _authenticatorService = authenticatorService;

    public Account? CurrentAccount { get; private set; }

    public bool IsLoggined => CurrentAccount is not null;

    public event Action StateChanged;

    public async Task<bool> Login(string username, string password)
    {
        var succes = true;

        try
        {
            CurrentAccount = await _authenticatorService.Login(username, password);
        }
        catch (Exception)
        {
            succes = false;
        }

        return succes;
    }

    public void Logout() => CurrentAccount = null;

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword) =>
        await _authenticatorService.Register(email, username, password, confirmPassword);
}
