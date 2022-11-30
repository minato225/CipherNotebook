namespace CipherNoteBook.Server.Models.SecurityModels;

public class DataResponse
{
    public string EncryptedSessionKey { get; set; }
    public string EncryptedFileText { get; set; }
    public string ErrorMessage { get; set; }
}
