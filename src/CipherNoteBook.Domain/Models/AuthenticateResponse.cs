using System.Text.Json.Serialization;

namespace CipherNoteBook.Domain.Models;

public class AuthenticateResponse
{
    [JsonConstructor]
    public AuthenticateResponse() { }
    public AuthenticateResponse(User user, string jwtToken, string refreshToken)
    {
        Id = user.Id;
        Email = user.Email;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        ErrorMessage = string.Empty;
    }

    [JsonInclude] public int Id { get; set; }
    [JsonInclude] public string Email { get; set; }
    [JsonInclude] public string JwtToken { get; set; }
    [JsonInclude] public string RefreshToken { get; set; }
    [JsonInclude] public string ErrorMessage { get; set; }
}