using Client.DataBase.Models;
using System.Threading.Tasks;

namespace Client.WPF.Services.AuthService.interfaces;

public interface IAuthenticatorService
{
    Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
    Task<Account> Login(string username, string password);
}

public enum RegistrationResult
{
    Success, Failure,
    PasswordsDoNotMatch,
    EmailAlreadyExists,
    UsernameAlreadyExists
}
