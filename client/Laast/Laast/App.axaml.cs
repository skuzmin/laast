using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using Laast.Services;
using Laast.ViewModels;
using Laast.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Laast;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Directory.CreateDirectory(Constants.AppDataPath);
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var services = serviceCollection.BuildServiceProvider();
        services.GetRequiredService<IAppConfigService>().InitConfig();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Services
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ILogger, AppLogger>();
        services.AddSingleton<IPilotService, PilotService>();
        services.AddSingleton<IAppConfigService, AppConfigService>();
        // ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<PilotViewModel>();
        services.AddTransient<PilotConfigViewModel>();
        services.AddTransient<WindowsViewModel>();
        services.AddTransient<CommandsViewModel>();
        services.AddTransient<SettingsViewModel>();
        // View
        services.AddSingleton<MainWindow>();
    }
}