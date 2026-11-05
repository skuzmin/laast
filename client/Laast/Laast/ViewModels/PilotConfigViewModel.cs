using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Enums;
using Laast.Models;
using Laast.Services;

namespace Laast.ViewModels;

public partial class PilotConfigViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private bool IsEditMode { get; }
    private PilotConfiguration _config;
    public ObservableCollection<PilotAction> Actions { get; }

    [ObservableProperty] private string _name;

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
        Actions = [.. pilotConfiguration.Actions.Select(item => item.Clone())];
        IsEditMode = isEditMode;
        _name = pilotConfiguration.Name;
        _config = pilotConfiguration;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void Back() => _navigationService.TryToNavigate(AppPage.Pilot);

    [RelayCommand]
    private void AddNewAction() => Actions.Add(new PilotAction());
    
    [RelayCommand]
    private void Save() => _navigationService.TryToNavigate(AppPage.Pilot);
}