namespace Server.Model
{
    public class DataResponse
    {
        public string EncryptedSessionKey { get; set; }
        public string EncryptedFileText { get; set; }
        public string ErrorMessage { get; set; }
    }
}
