using System.Diagnostics;
using AutoUpdateChoco.Contracts;

namespace AutoUpdateChoco.Services
{
    public class ChocoService : IChocoService
    {
        public async Task<bool> UpgradeAllAsync()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "choco",
                    Arguments = "upgrade all -y",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                var output = await process!.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Ausführen von choco: {ex.Message}");
                return false;
            }
        }
    }
}
