namespace CipherNoteBook.Domain.Models;

public class Message
{
    public string? EncryptedSessionKey { get; set; }
    public string? EncryptedFileText { get; set; }
    public string? ErrorMessage { get; set; }
}
