using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Enums;
using Laast.Models;
using Laast.Services;

namespace Laast.ViewModels;

public partial class PilotViewModel : ViewModelBase
{
    [ObservableProperty] private PilotConfiguration? _selectedPilotConfiguration;
    public ObservableCollection<PilotConfiguration> Configs { get; set; }
    public INavigationService NavigationService { get; }
    public ICommand SelectConfigCommand { get; }
    public ICommand EditConfigCommand { get; }
    public ICommand DeleteConfigCommand { get; }

    public PilotViewModel(INavigationService navigationService, IPilotService pilotService)
    {
        NavigationService = navigationService;
        EditConfigCommand =
            new RelayCommand<PilotConfiguration>(config =>
                navigationService.TryToNavigate(AppPage.PilotConfig, config));
        SelectConfigCommand = new RelayCommand<PilotConfiguration>(config =>
        {
            pilotService.SelectPilotConfiguration(config);
            UpdateSelectedPilotConfiguration(pilotService);
        });
        DeleteConfigCommand =
            new RelayCommand<PilotConfiguration>(config =>
            {
                pilotService.DeletePilotConfiguration(config);
                UpdateSelectedPilotConfiguration(pilotService);
                Configs!.Remove(config!);
            });
        Configs = new ObservableCollection<PilotConfiguration>(pilotService.GetPilotConfigurations());
        UpdateSelectedPilotConfiguration(pilotService);
    }

    private void UpdateSelectedPilotConfiguration(IPilotService pilotService)
    {
        SelectedPilotConfiguration = pilotService.GetSelectedPilotConfiguration();
    }
}