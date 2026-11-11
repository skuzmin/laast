using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Client.Enums;
using Laast.Client.Models;
using Laast.Client.Services;

namespace Laast.Client.ViewModels;

public partial class PilotViewModel : ViewModelBase
{
    private readonly IPilotService _pilotService;
    public INavigationService NavigationService { get; }
    public ObservableCollection<PilotConfiguration> Configs { get; set; }

    [ObservableProperty] private PilotConfiguration? _selectedPilotConfiguration;

    public PilotViewModel(INavigationService navigationService, IPilotService pilotService)
    {
        NavigationService = navigationService;
        _pilotService = pilotService;
        Configs = [.. pilotService.Configurations];
        UpdateSelectedPilotConfiguration(pilotService);
    }

    [RelayCommand]
    private void SelectConfig(PilotConfiguration config)
    {
        _pilotService.SelectConfiguration(config);
        UpdateSelectedPilotConfiguration(_pilotService);
    }

    [RelayCommand]
    private void EditConfig(PilotConfiguration config) =>
        NavigationService.TryToNavigate(AppPage.PilotConfig, config);

    [RelayCommand]
    private void DeleteConfig(PilotConfiguration config)
    {
        _pilotService.DeleteConfiguration(config);
        UpdateSelectedPilotConfiguration(_pilotService);
        Configs.Remove(config);
    }

    private void UpdateSelectedPilotConfiguration(IPilotService pilotService)
    {
        SelectedPilotConfiguration = pilotService.SelectedConfiguration;
    }
}