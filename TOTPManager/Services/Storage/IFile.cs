using System.Security.Cryptography;

namespace TOTPManager.Services.Storage
{
    public interface IFile
    {
        string TempFolder { get; }

        void WriteAllText(string filePath, string text);

        void WriteAllBytes(string filePath, byte[] bytes);

        string ReadAllText(string filePath);

        byte[] ReadAllBytes(string filePath);

        bool Exists(string filePath);

        void Delete(string filePath);
    }
}
