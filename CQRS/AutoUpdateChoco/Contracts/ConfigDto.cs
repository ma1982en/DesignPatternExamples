namespace AutoUpdateChoco.Contracts
{
    public class ConfigDto
    {
        public bool EnableAutostart { get; set; }
        public int UpdateIntervalMinutes { get; set; }
        public bool ShowNotifications { get; set; }
        public string ConfigPath { get; set; } = string.Empty;
    }
}
