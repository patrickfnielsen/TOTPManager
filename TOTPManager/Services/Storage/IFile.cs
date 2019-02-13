namespace TOTPManager.Services.Storage
{
    public interface IFile
    {
        string TempFolder { get; }

        void Save(string filePath, string text);

        string ReadAllText(string filePath);

        bool Exists(string filePath);

        void Delete(string filePath);
    }
}
