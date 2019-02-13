using System;
using System.IO;

namespace TOTPManager.Services.Storage
{
    public class FileStorage : IFile
    {
        public string TempFolder => $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TOTPManager")}";

        public void WriteAllText(string filePath, string text) => File.WriteAllText(filePath, text);

        public void WriteAllBytes(string filePath, byte[] bytes) => File.WriteAllBytes(filePath, bytes);

        public string ReadAllText(string filePath) => File.ReadAllText(filePath);

        public byte[] ReadAllBytes(string filePath) => File.ReadAllBytes(filePath);

        public bool Exists(string filePath) => File.Exists(filePath);

        public void Delete(string filePath) => File.Delete(filePath);
    }
}
