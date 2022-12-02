namespace CipherNoteBook.Domain.Exceptions;

public class InvalidOtpCodeException : Exception
{
    public string Username { get; set; }
    public string OtpCode { get; set; }

    public InvalidOtpCodeException(string username, string otpCode) =>
        (Username, OtpCode) = (username, otpCode);

    public InvalidOtpCodeException(string message, string username, string otpCode)
        : base(message) =>
        (Username, OtpCode) = (username, otpCode);

    public InvalidOtpCodeException(string message, Exception innerException, string username, string otpCode)
        : base(message, innerException) =>
        (Username, OtpCode) = (username, otpCode);
}
