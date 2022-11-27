using Client.Models;
using System.Threading.Tasks;

namespace Client.Services.AuthService;

public interface IAuthenticatorService
{
    Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
    Task<Account> Login(string username, string password);
}

public enum RegistrationResult
{
    Success, Failure
}
