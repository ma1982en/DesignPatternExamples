using AutoUpdateChoco.Configuration;
using AutoUpdateChoco.Contracts;
using AutoUpdateChoco.Queries;

namespace AutoUpdateChoco.Handlers
{
    public class GetConfigQueryHandler : IQueryHandler<GetConfigQuery, ConfigDto>
    {
        private readonly AppConfig _config;

        public GetConfigQueryHandler(AppConfig config)
        {
            _config = config;
        }

        public Task<ConfigDto> Handle(GetConfigQuery query)
        {
            return Task.FromResult(new ConfigDto
            {
                EnableAutostart = _config.EnableAutostart,
                UpdateIntervalMinutes = _config.UpdateIntervalMinutes,
                ShowNotifications = _config.ShowNotifications,
                ConfigPath = _config.ConfigPath
            });
        }
    }
}
