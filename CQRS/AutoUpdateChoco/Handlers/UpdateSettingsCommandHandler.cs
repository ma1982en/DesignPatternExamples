using AutoUpdateChoco.Commands;
using AutoUpdateChoco.Configuration;
using AutoUpdateChoco.Contracts;
using Microsoft.Extensions.Logging;

namespace AutoUpdateChoco.Handlers
{
    public class UpdateSettingsCommandHandler : ICommandHandler<UpdateSettingsCommand, ConfigDto>
    {
        private readonly AppConfig _config;
        private readonly IAutostartService _autostartService;
        private readonly ILogger<UpdateSettingsCommandHandler> _logger;

        public UpdateSettingsCommandHandler(AppConfig config, IAutostartService autostartService, ILogger<UpdateSettingsCommandHandler> logger)
        {
            _config = config;
            _autostartService = autostartService;
            _logger = logger;
        }

        public Task<ConfigDto> Handle(UpdateSettingsCommand command)
        {
            var changed = false;

            if (command.EnableAutostart.HasValue)
            {
                _config.EnableAutostart = command.EnableAutostart.Value;
                if (_config.EnableAutostart)
                    _autostartService.RegisterAutostart(Environment.ProcessPath!);
                else
                    _autostartService.UnregisterAutostart();
                changed = true;
            }

            if (command.UpdateIntervalMinutes.HasValue)
            {
                _config.UpdateIntervalMinutes = Math.Max(5, command.UpdateIntervalMinutes.Value);
                changed = true;
            }

            if (command.ShowNotifications.HasValue)
            {
                _config.ShowNotifications = command.ShowNotifications.Value;
                changed = true;
            }

            if (changed)
            {
                _config.Save();
                _logger.LogInformation("Konfiguration gespeichert: Autostart={EnableAutostart}, Interval={Interval}, Notifications={Notifications}",
                    _config.EnableAutostart, _config.UpdateIntervalMinutes, _config.ShowNotifications);
            }

            var dto = new ConfigDto
            {
                EnableAutostart = _config.EnableAutostart,
                UpdateIntervalMinutes = _config.UpdateIntervalMinutes,
                ShowNotifications = _config.ShowNotifications,
                ConfigPath = _config.ConfigPath
            };

            return Task.FromResult(dto);
        }
    }
}
