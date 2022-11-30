using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Server.Models.AuthModels;
using Microsoft.Extensions.Options;

namespace CipherNoteBook.Server.AuthUtils;

public class JwtMiddleware
{
    private readonly AppSettings _appSettings;
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IAccountService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);
        if (userId is not null)
            context.Items["User"] = userService.Get(userId.Value);

        await _next(context);
    }
}