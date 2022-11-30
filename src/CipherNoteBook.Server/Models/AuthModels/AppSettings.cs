namespace CipherNoteBook.Server.Models.AuthModels;

public class AppSettings
{
    public string Secret { get; set; }
    public int RefreshTokenTtl { get; set; }
    public string RegisterUrl { get; set; }
}