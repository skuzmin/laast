using System.ComponentModel;
using Laast.Enums;
using Laast.ViewModels;

namespace Laast.Services;

public interface INavigationService : INotifyPropertyChanged
{
    ViewModelBase? CurrentPage { get; }
    void NavigateToDefaultPage();
    bool TryToNavigate(AppPage page);
    bool IsPilotActive { get; }
    bool IsWindowsActive { get; }
    bool IsCommandsActive { get; }
    bool IsSettingsActive { get; }
}