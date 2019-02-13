using System;
using System.IO;

namespace TOTPManager.Services.Storage
{
    public class FileStorage : IFile
    {
        public string TempFolder => $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TOTPManager")}";

        public void Save(string filePath, string text) => File.WriteAllText(filePath, text);

        public string ReadAllText(string filePath) => File.ReadAllText(filePath);

        public bool Exists(string filePath) => File.Exists(filePath);

        public void Delete(string filePath) => File.Delete(filePath);
    }
}
