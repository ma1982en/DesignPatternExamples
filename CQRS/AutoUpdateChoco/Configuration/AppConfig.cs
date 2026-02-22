using System.Text.Json;

namespace AutoUpdateChoco.Configuration
{
    public class AppConfig
    {
        public bool EnableAutostart { get; set; } = true;
        public int UpdateIntervalMinutes { get; set; } = 60;
        public bool ShowNotifications { get; set; } = true;
        public string ConfigPath { get; set; } = "config.json";

        public void Save()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }

        public static AppConfig Load(string path = "config.json")
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
                config.ConfigPath = path;
                return config;
            }

            var newConfig = new AppConfig { ConfigPath = path };
            newConfig.Save();
            return newConfig;
        }
    }
}
