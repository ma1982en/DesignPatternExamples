
using AutoUpdateChoco.Commands;
using AutoUpdateChoco.Contracts;
using Microsoft.Extensions.Logging;


namespace AutoUpdateChoco.Handlers
{
    public class UpgradeChocoCommandHandler : ICommandHandler<UpgradeChocoCommand, bool>
    {
        private readonly IChocoService _chocoService;
        private readonly ILogger<UpgradeChocoCommandHandler> _logger;

        public UpgradeChocoCommandHandler(IChocoService chocoService, ILogger<UpgradeChocoCommandHandler> logger)
        {
            _chocoService = chocoService;
            _logger = logger;
        }

        public async Task<bool> Handle(UpgradeChocoCommand command)
        {
            _logger.LogInformation("Starte Chocolatey-Update...");
            var success = await _chocoService.UpgradeAllAsync();
        
            if (!success)
                throw new InvalidOperationException("Choco-Update fehlgeschlagen");

            _logger.LogInformation("✓ Update erfolgreich abgeschlossen!");
            return true;
        }
    }
}
