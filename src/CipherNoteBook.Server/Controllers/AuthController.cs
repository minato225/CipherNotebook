using CipherNoteBook.Domain.Models;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Server.AuthUtils;
using CipherNoteBook.Server.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;

using BCryptNet = BCrypt.Net.BCrypt;

namespace CipherNoteBook.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IAccountService _accountService;
    private readonly IJwtUtils _jwtUtils;

    public AuthController(IAuthService authService, IAccountService userService, IJwtUtils jwtUtils)
    {
        _authService = authService;
        _accountService = userService;
        _jwtUtils = jwtUtils;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRequest userRequest)
    {
        var user = new User
        {
            DatedJoined = DateTime.Now,
            Username = userRequest.UserName,
            Email = userRequest.Email,
            AuthToken = "test",
            PasswordHash = BCryptNet.HashPassword(userRequest.Password),
            RefreshTokens = new List<RefreshToken>()
        };

        var res = await Task.Run(() => _authService.Register(user, IpAddress()));

        return Ok(res);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequestModel model)
    {
        var response = _authService.Login(model, IpAddress());

        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("users")]
    public IActionResult Users() => Ok(_accountService.GetAll());

    [HttpGet("user")]
    public async Task<IActionResult> CurrentUsers()
    {
        Request.Headers.TryGetValue("Authorization", out var token);
        var userId = _jwtUtils.ValidateJwtToken(token) ?? 0;
        var user = await _accountService.Get(userId);
        return Ok(user);
    }

    [HttpGet("EmailUser")]
    public async Task<IActionResult> UserByEmail(string email)
    {
        Request.Headers.TryGetValue("Authorization", out var token);
        var userId = _jwtUtils.ValidateJwtToken(token) ?? 0;
        var user = await _accountService.GetByEmail(email);
        return Ok(user ?? new User());
    }

    #region helper methods

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        if (token is not null)
            Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string IpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }

    #endregion
}
