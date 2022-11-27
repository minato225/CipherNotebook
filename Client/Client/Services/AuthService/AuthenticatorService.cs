using Client.Models;
using System;
using System.Threading.Tasks;

namespace Client.Services.AuthService
{
    public class AuthenticatorService : IAuthenticatorService
    {
        public Task<Account> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}
