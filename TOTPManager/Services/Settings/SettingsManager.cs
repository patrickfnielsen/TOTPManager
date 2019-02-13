using Newtonsoft.Json;
using TOTPManager.Models;
using TOTPManager.Services.Storage;

namespace TOTPManager.Services.Settings
{
    public class SettingsManager : ISettings
    {
        private readonly IFile _file;
        private const string FileName = "settings.json";

        public SettingsManager(IFile file)
        {
            _file = file;
        }

        public void Save(AppSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            _file.WriteAllText($@"{_file.TempFolder}\{FileName}", json);
        }

        public AppSettings Load()
        {
            if (!_file.Exists($@"{_file.TempFolder}\{FileName}"))
                return new AppSettings();

            var json = _file.ReadAllText($@"{_file.TempFolder}\{FileName}");

            return JsonConvert.DeserializeObject<AppSettings>(json);
        }
    }
}
