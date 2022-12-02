namespace CipherNoteBook.Domain.Services.OtpService;

public interface IOtpService
{
    bool VerifyOtpCode(string otpCode);
}
