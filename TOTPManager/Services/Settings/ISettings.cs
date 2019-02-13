using TOTPManager.Models;

namespace TOTPManager.Services.Settings
{
    public interface ISettings
    {
        void Save(AppSettings settings);
        AppSettings Load();
    }
}
