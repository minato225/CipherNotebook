using Client.Models;
using Client.Services.AuthService;
using System.Threading.Tasks;

namespace Client.State.Authenticators;

public interface IAuthenticator
{
    Account CurrentAccount { get; }
    bool IsLoggined { get; }

    Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
    Task<bool> Login(string username, string password);
    void Logout();
}
