using NETCore.Encrypt;
using System.IO;

namespace Client
{
    public class CipherHelper
    {
        public static string DecryptSessionKey(string encryptedFileText, string privateKey, string decryptedSessionKey)
        {
            if (privateKey is null || decryptedSessionKey is null) return string.Empty;
            var sessionKey = EncryptProvider.RSADecrypt(privateKey, decryptedSessionKey);

            return EncryptProvider.AESDecrypt(encryptedFileText, sessionKey);
        }

        public static string GenerateRsaOpenKey()
        {
            var rsaKey = EncryptProvider.CreateRsaKey();
            File.WriteAllText("PrivateKey.txt", rsaKey.PrivateKey);

            return rsaKey.PublicKey;
        }
    }
}
