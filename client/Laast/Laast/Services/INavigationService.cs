using System.ComponentModel;
using System.Windows.Input;
using Laast.Enums;
using Laast.ViewModels;

namespace Laast.Services;

public interface INavigationService : INotifyPropertyChanged
{
    ViewModelBase? CurrentPage { get; }
    public ICommand NavigateToPageCommand { get; }
    void NavigateToDefaultPage();
    bool TryToNavigate(AppPage page, object? parameter = null);
    bool IsPilotActive { get; }
    bool IsWindowsActive { get; }
    bool IsCommandsActive { get; }
    bool IsSettingsActive { get; }
}