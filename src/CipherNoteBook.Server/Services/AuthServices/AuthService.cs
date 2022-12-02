using CipherNoteBook.Domain.Models;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Domain.Services.OtpService;
using CipherNoteBook.Server.AuthUtils;
using CipherNoteBook.Server.Models.AuthModels;
using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CipherNoteBook.Server.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly AppSettings _appSettings;
    private readonly IJwtUtils _jwtUtils;
    private readonly IAccountService _accountService;
    private readonly IOtpService _otpService;

    public AuthService(
        IJwtUtils jwtUtils, 
        IOptions<AppSettings> appSettings, 
        IAccountService accountService,
        IOtpService otpService)
    {
        _jwtUtils = jwtUtils;
        _appSettings = appSettings.Value;
        _accountService = accountService;
        _otpService = otpService;
    }

    public AuthenticateResponse Login(LoginRequestModel model, string ipAddress)
    {
        var user = _accountService.GetByEmail(model.Email).Result;

        if (user is null)
            return new AuthenticateResponse { ErrorMessage = "UserNotFoundException" };

        if (!BCryptNet.Verify(model.Password, user.PasswordHash))
            return new AuthenticateResponse { ErrorMessage = "InvalidPasswordException" };


        if (!_otpService.VerifyOtpCode(model.OtpCode))
            return new AuthenticateResponse { ErrorMessage = "InvalidOtpCodeException" };

        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        user.RefreshTokens.Add(refreshToken);

        RemoveOldRefreshTokens(user);

        _accountService.Update(user.Id, new Account { AccountHolder = user });

        return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
    }

    public AuthenticateResponse Register(User model, string ipAddress)
    {

        var jwtToken = _jwtUtils.GenerateJwtToken(model);
        var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        model.RefreshTokens.Add(refreshToken);
        RemoveOldRefreshTokens(model);

        var acc = _accountService.Create(new Account { AccountHolder = model }).Result;
        _accountService.Update(model.Id, acc);

        return new AuthenticateResponse(model, jwtToken, refreshToken.Token);
    }

    #region helper methods

    private void RemoveOldRefreshTokens(User user)
    {
        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTtl) <= DateTime.UtcNow);
    }
    #endregion
}