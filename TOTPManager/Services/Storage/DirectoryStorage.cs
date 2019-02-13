using System.IO;

namespace TOTPManager.Services.Storage
{
    public class DirectoryStorage : IDirectory
    {
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);
    }
}
