using Microsoft.AspNetCore.Identity;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Domain.Models;
using CipherNoteBook.Domain.Exceptions;

namespace CipherNoteBook.Domain.Services.AuthService;

public class AuthenticatorService : IAuthenticatorService
{
    private readonly IAccountService _accountService;
    private readonly IPasswordHasher<Account> _passwordHasher;

    public AuthenticatorService(IAccountService accountService, IPasswordHasher<Account> passwordHasher)
    {
        _accountService = accountService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Account> Login(string username, string password)
    {
        var storedAccount = await _accountService.GetByUsername(username);

        if (storedAccount is null)
        {
            throw new UserNotFoundException(username);
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(storedAccount, storedAccount.AccountHolder.PasswordHash, password);

        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new InvalidPasswordException(username, password);
        }

        return storedAccount;
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
    {
        var result = RegistrationResult.Success;

        if (password != confirmPassword)
        {
            result = RegistrationResult.PasswordsDoNotMatch;
        }

        var emailAccount = await _accountService.GetByEmail(email);
        if (emailAccount != null)
        {
            result = RegistrationResult.EmailAlreadyExists;
        }

        var usernameAccount = await _accountService.GetByUsername(username);
        if (usernameAccount != null)
        {
            result = RegistrationResult.UsernameAlreadyExists;
        }

        if (result == RegistrationResult.Success)
        {
            string hashedPassword = _passwordHasher.HashPassword(usernameAccount, password);

            var user = new User
            {
                Email = email,
                Username = username,
                PasswordHash = hashedPassword,
                AuthToken = "test",
                DatedJoined = DateTime.Now
            };

            var account = new Account
            {
                AccountHolder = user
            };

            await _accountService.Create(account);
        }

        return result;
    }
}
