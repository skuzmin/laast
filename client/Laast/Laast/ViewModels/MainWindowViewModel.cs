using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Laast.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PilotViewModel _pilot = new();
    private readonly WindowsViewModel _windows = new();
    private readonly CommandsViewModel _commands = new();
    private readonly SettingsViewModel _settings = new();
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPilotActive))]
    [NotifyPropertyChangedFor(nameof(IsWindowsActive))]
    [NotifyPropertyChangedFor(nameof(IsCommandsActive))]
    [NotifyPropertyChangedFor(nameof(IsSettingsActive))]
    private ViewModelBase? _currentPage;
    public bool IsPilotActive => CurrentPage == _pilot;
    public bool IsWindowsActive => CurrentPage == _windows;
    public bool IsCommandsActive => CurrentPage == _commands;
    public bool IsSettingsActive => CurrentPage == _settings;
    public MainWindowViewModel()
    {
        CurrentPage = _pilot;
    }

    [RelayCommand] private void GoPilot() => CurrentPage = _pilot;
    [RelayCommand] private void GoWindows() => CurrentPage = _windows;
    [RelayCommand] private void GoCommands() => CurrentPage = _commands;
    [RelayCommand] private void GoSettings() => CurrentPage = _settings;
}