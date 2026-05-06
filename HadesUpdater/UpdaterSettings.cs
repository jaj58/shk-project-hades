using System;
using System.IO;
using System.Web.Script.Serialization;

namespace HadesUpdater
{
    public class UpdaterSettings
    {
        public string LicenseKey       { get; set; } = string.Empty;
        public string InstallDir       { get; set; } = string.Empty;
        public string InstalledVersion { get; set; } = string.Empty;  // tracked by updater, not read from exe

        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(LicenseKey) &&
            !string.IsNullOrWhiteSpace(InstallDir);

        // ------------------------------------------------------------------ //

        private static string SettingsDir =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "HadesUpdater");

        private static string SettingsPath =>
            Path.Combine(SettingsDir, "settings.json");

        public static UpdaterSettings Load()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    string json = File.ReadAllText(SettingsPath);
                    var s = new JavaScriptSerializer().Deserialize<UpdaterSettings>(json);
                    if (s != null) return s;
                }
            }
            catch { /* fall through to default */ }
            return new UpdaterSettings();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(SettingsDir);
                string json = new JavaScriptSerializer().Serialize(this);
                File.WriteAllText(SettingsPath, json);
            }
            catch { /* non-fatal */ }
        }
    }
}
