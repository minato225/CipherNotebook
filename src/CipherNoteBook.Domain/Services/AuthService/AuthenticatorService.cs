using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Domain.Models;
using CipherNoteBook.Domain.Exceptions;
using System.Net.Http.Json;

namespace CipherNoteBook.Domain.Services.AuthService;

public class AuthenticatorService : IAuthenticatorService
{
    private readonly HttpClient _httpClient;

    public AuthenticatorService(HttpClient httpClient) =>
        _httpClient = httpClient;

    public async Task<Account> Login(string username, string password, string otpCode)
    {
        var model = new LoginRequestModel
        {
            Email = username,
            Password = password,
            OtpCode = otpCode
        };

        var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", model);
        var req = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();

        if (req?.ErrorMessage == "UserNotFoundException")
        {
            throw new UserNotFoundException(username);
        }

        if (req?.ErrorMessage == "InvalidPasswordException")
        {
            throw new InvalidPasswordException(username, password);
        }

        if (req?.ErrorMessage == "InvalidOtpCodeException")
        {
            throw new InvalidOtpCodeException(username, otpCode);
        }

        return new Account
        {
            AccountHolder = new User
            {
                Email = req.Email,
                Username = username
            }
        };
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            return RegistrationResult.PasswordsDoNotMatch;
        }

        var user = await _httpClient.GetFromJsonAsync<User>($"/api/Auth/EmailUser?email={email}");
        if (user?.Username is not null)
        {
            return RegistrationResult.EmailAlreadyExists;
        }

        var model = new UserRequest { Email = email, Password = password, UserName = username };
        var res = await _httpClient.PostAsJsonAsync("/api/Auth/register", model);
        var req = await res.Content.ReadFromJsonAsync<AuthenticateResponse>();

        return RegistrationResult.Success;
    }
}