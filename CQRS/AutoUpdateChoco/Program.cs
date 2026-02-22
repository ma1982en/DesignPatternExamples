using Microsoft.Extensions.Hosting;
using RazorConsole.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using AutoUpdateChoco.Configuration;
using AutoUpdateChoco.Mediators;
using AutoUpdateChoco.Commands;
using AutoUpdateChoco.Contracts;
using AutoUpdateChoco.Handlers;
using AutoUpdateChoco.Services;
using AutoUpdateChoco.Queries;

namespace AutoUpdateChoco
{
    [SupportedOSPlatform("windows")]
    class Program
    {
        static async Task Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Error.WriteLine("Dieses Programm wird nur auf Windows unterstützt.");
                Environment.Exit(1);
            }
            
            var config = AppConfig.Load();
            
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                // Config als Singleton registrieren
                services.AddSingleton(config);

                // Mediator registrieren
                services.AddSingleton<IMediator, Mediator>();

                // Services registrieren
                services.AddSingleton<IChocoService, ChocoService>();
                
                services.AddSingleton<IAutostartService, AutostartService>();

                // Handler registrieren (Commands)
                services.AddSingleton<UpgradeChocoCommandHandler>();
                services.AddSingleton<ICommandHandler<UpgradeChocoCommand, bool>>(sp => 
                    sp.GetRequiredService<UpgradeChocoCommandHandler>());
                services.AddSingleton<UpdateSettingsCommandHandler>();
                services.AddSingleton<ICommandHandler<UpdateSettingsCommand, ConfigDto>>(sp =>
                    sp.GetRequiredService<UpdateSettingsCommandHandler>());

                // Handler registrieren (Queries)
                services.AddSingleton<GetConfigQueryHandler>();
                services.AddSingleton<IQueryHandler<GetConfigQuery, ConfigDto>>(sp =>
                    sp.GetRequiredService<GetConfigQueryHandler>());
            });
            
            hostBuilder.UseRazorConsole<AutoUpdateChocoConfiguration>();
            
            var host = hostBuilder.Build();

            if (args.Length > 0 && args[0] == "--update")
            {
                // Hintergrund-Update
                try
                {
                    var mediator = host.Services.GetRequiredService<IMediator>();
                    await mediator.Send(new UpgradeChocoCommand());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler: {ex.Message}");
                    Environment.Exit(1);
                }
            }
            else
            {
                await host.RunAsync();
            }
        }
    }
}