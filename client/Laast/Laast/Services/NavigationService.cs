using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Laast.Enums;
using Laast.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Laast.Services;

internal partial class NavigationService : ObservableObject, INavigationService
{
    private readonly IServiceProvider _services;
    private readonly ILogger _logger;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPilotActive))]
    [NotifyPropertyChangedFor(nameof(IsWindowsActive))]
    [NotifyPropertyChangedFor(nameof(IsCommandsActive))]
    [NotifyPropertyChangedFor(nameof(IsSettingsActive))]
    private ViewModelBase? _currentPage;

    public bool IsPilotActive => CurrentPage is PilotViewModel;
    public bool IsWindowsActive => CurrentPage is WindowsViewModel;
    public bool IsCommandsActive => CurrentPage is CommandsViewModel;
    public bool IsSettingsActive => CurrentPage is SettingsViewModel;

    public NavigationService(IServiceProvider services, ILogger logger)
    {
        _services = services;
        _logger = logger;
    }

    public void NavigateToDefaultPage()
    {
        NavigateTo<PilotViewModel>();
    }

    public bool TryToNavigate(AppPage page)
    {
        switch (page)
        {
            case AppPage.Pilot:
                NavigateTo<PilotViewModel>();
                break;
            case AppPage.Windows:
                NavigateTo<WindowsViewModel>();
                break;
            case AppPage.Commands:
                NavigateTo<CommandsViewModel>();
                break;
            case AppPage.Settings:
                NavigateTo<SettingsViewModel>();
                break;
            default:
                NavigateToDefaultPage();
                break;
        }

        return true;
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        CurrentPage = _services.GetRequiredService<T>();
        _logger.LogInformation("Navigate to page: {page}", typeof(T).Name);
    }
}