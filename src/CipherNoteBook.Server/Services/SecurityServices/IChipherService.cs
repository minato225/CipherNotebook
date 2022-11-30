namespace CipherNoteBook.Server.Services.SecurityServices;

public interface IChipherService
{
    string AesEncrypt(string text, string sessionKey);
    string AesDecrypt(string encryptText);

    string RsaEncrypt(string text, string OpenRsaKey);

    string GenerateKey();
}
