using CipherNoteBook.Domain.Models;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using System;
using System.Threading.Tasks;

namespace CipherNoteBook.OTP.State.Authenticators;

public class Authenticator : IAuthenticator
{
    private IAuthenticatorService _authenticatorService;

    public Authenticator(IAuthenticatorService authenticatorService) => _authenticatorService = authenticatorService;

    public Account? CurrentAccount { get; private set; }

    public bool IsLoggined => CurrentAccount is not null;

    public event Action StateChanged;

    public async Task Login(string username, string password) =>
        CurrentAccount = await _authenticatorService.Login(username, password, string.Empty);

    public void Logout() => CurrentAccount = null;
}
