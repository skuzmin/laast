using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Laast.Enums;
using Laast.Models;
using Laast.Services;

namespace Laast.ViewModels;

public class PilotConfigViewModel : ViewModelBase
{
    public INavigationService NavigationService { get; }
    public ICommand BackCommand { get; }
    public string Title => IsEditMode ? $"Edit pilot config: [ {Config.Name} ]" : "New pilot config";
    public PilotConfiguration Config { get; }

    private bool IsEditMode { get; }

    public PilotConfigViewModel(INavigationService navigationService)
        : this(navigationService, new PilotConfiguration(), isEditMode: false)
    {
    }

    public PilotConfigViewModel(INavigationService navigationService, PilotConfiguration pilotConfiguration)
        : this(navigationService, pilotConfiguration, isEditMode: true)
    {
    }

    private PilotConfigViewModel(INavigationService navigationService, PilotConfiguration pilotConfiguration,
        bool isEditMode)
    {
        Config = pilotConfiguration;
        IsEditMode = isEditMode;
        NavigationService = navigationService;
        BackCommand = new RelayCommand(() => navigationService.TryToNavigate(AppPage.Pilot));
    }
}