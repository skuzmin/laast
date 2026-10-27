using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Enums;
using Laast.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Laast.Services;

internal partial class NavigationService : ObservableObject, INavigationService
{
    private readonly IServiceProvider _services;
    private readonly ILogger _logger;
    public ICommand NavigateToPageCommand { get; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPilotActive))]
    [NotifyPropertyChangedFor(nameof(IsWindowsActive))]
    [NotifyPropertyChangedFor(nameof(IsCommandsActive))]
    [NotifyPropertyChangedFor(nameof(IsSettingsActive))]
    private ViewModelBase? _currentPage;

    public bool IsPilotActive => CurrentPage is PilotViewModel or PilotConfigViewModel;
    public bool IsWindowsActive => CurrentPage is WindowsViewModel;
    public bool IsCommandsActive => CurrentPage is CommandsViewModel;
    public bool IsSettingsActive => CurrentPage is SettingsViewModel;

    public NavigationService(IServiceProvider services, ILogger logger)
    {
        _services = services;
        _logger = logger;
        NavigateToPageCommand = new RelayCommand<AppPage>(
            page => TryToNavigate(page),
            canExecute: CanNavigateToPage);
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
            case AppPage.PilotConfig:
                NavigateTo<PilotConfigViewModel>();
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

    public bool CanNavigateToPage(AppPage page)
    {
        return page switch
        {
            AppPage.Pilot => !IsPilotActive,
            AppPage.Settings => !IsSettingsActive,
            AppPage.Windows => !IsWindowsActive,
            AppPage.Commands => !IsCommandsActive,
            _ => true
        };
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        CurrentPage = _services.GetRequiredService<T>();
        _logger.LogInformation("Navigate to page: {page}", typeof(T).Name);
    }
}