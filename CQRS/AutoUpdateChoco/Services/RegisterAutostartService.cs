using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.Runtime.Versioning;
using AutoUpdateChoco.Contracts;

namespace AutoUpdateChoco.Services;

[SupportedOSPlatform("windows")]
public class AutostartService : IAutostartService
{
    private readonly ILogger<AutostartService> _logger;

    public AutostartService(ILogger<AutostartService> logger)
    {
        _logger = logger;
    }

    public void RegisterAutostart(string appPath)
    {
        try
        {
            var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);
            
            key?.SetValue("AutoUpdateChoco", appPath);
            _logger.LogInformation("✓ Im Autostart registriert!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "✗ Autostart-Fehler bei Registrierung");
        }
    }

    public void UnregisterAutostart()
    {
        try
        {
            var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);
            
            key?.DeleteValue("AutoUpdateChoco", false);
            _logger.LogInformation("✓ Aus Autostart entfernt!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "✗ Autostart-Fehler bei De-Registrierung");
        }
    }
}
