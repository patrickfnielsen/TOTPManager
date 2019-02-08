namespace TOTPManager.Services.Encryption
{
    public interface ITextEncryption
    {
        byte[] Encrypt(string text);
        string Decrypt(byte[] encryptedBytes);
    }
}
