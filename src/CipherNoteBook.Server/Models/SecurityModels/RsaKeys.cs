namespace CipherNoteBook.Server.Models.SecurityModels;

public class RsaKeys
{
    public string OpenKey { get; set; }

    public RsaKeys() => OpenKey = "ROMAN_SECRET_KEY";
}
