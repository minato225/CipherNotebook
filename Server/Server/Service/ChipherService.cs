using NETCore.Encrypt;

namespace Server.Servers
{
    public class ChipherService : IChipherService
    {
        public string AesDecrypt(string encryptText)
        {
            return encryptText;
        }

        public string AesEncrypt(string text, string sessionKey)
        {
            return EncryptProvider.AESEncrypt(
                data: text,
                key: sessionKey);
        }

        public string GenerateKey()
        {
            return EncryptProvider.CreateAesKey().Key;
        }

        public string RsaEncrypt(string text, string OpenRsaKey)
        {
            return EncryptProvider.RSAEncrypt(
                publicKey: OpenRsaKey,
                srcString: text);
        }
    }
}