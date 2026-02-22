using AutoUpdateChoco.Contracts;

namespace AutoUpdateChoco.Commands
{
    public class UpdateSettingsCommand : ICommand<ConfigDto>
    {
        public bool? EnableAutostart { get; set; }
        public int? UpdateIntervalMinutes { get; set; }
        public bool? ShowNotifications { get; set; }
    }
}
