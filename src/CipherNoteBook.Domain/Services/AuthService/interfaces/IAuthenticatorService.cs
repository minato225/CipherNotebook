using CipherNoteBook.Domain.Models;

namespace CipherNoteBook.Domain.Services.AuthService.interfaces;

public interface IAuthenticatorService
{
    Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
    Task<Account> Login(string username, string password, string otpCode);
}

public enum RegistrationResult
{
    Success,
    Failure,
    PasswordsDoNotMatch,
    EmailAlreadyExists,
    UsernameAlreadyExists
}
