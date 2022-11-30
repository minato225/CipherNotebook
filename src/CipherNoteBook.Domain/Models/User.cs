namespace CipherNoteBook.Domain.Models;

public class User : DomainObject
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string AuthToken { get; set; }
    public DateTime DatedJoined { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; }
}
