using System.Security.Cryptography;
using System.Text;

namespace TOTPManager.Services.Encryption
{
    public class WindowTextEncryption : ITextEncryption
    {
        private byte[] _staticEntropy = { 0x6c, 0x5d, 0x1d, 0x66, 0xf9, 0xc7, 0x12, 0x0a, 0x35, 0x24, 0x5c, 0x8b, 0x3d, 0x9b, 0x32, 0xf0 };

        public string Decrypt(byte[] encryptedBytes)
        {
            var unprotectedByteArray = ProtectedData.Unprotect(encryptedBytes, _staticEntropy, DataProtectionScope.CurrentUser);
            var unprotectedString = Encoding.ASCII.GetString(unprotectedByteArray);

            return unprotectedString;
        }

        public byte[] Encrypt(string text)
        {
            var bytesToProtect = Encoding.ASCII.GetBytes(text);
            var encryptedBytes = ProtectedData.Protect(bytesToProtect, _staticEntropy, DataProtectionScope.CurrentUser);

            return encryptedBytes;
        }
    }
}
