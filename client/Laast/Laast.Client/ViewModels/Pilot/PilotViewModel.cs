using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Laast.Client.Enums;
using Laast.Client.Models;
using Laast.Client.Services;
using Laast.Client.ViewModels.Common;

namespace Laast.Client.ViewModels.Pilot;

public partial class PilotViewModel : ViewModelBase
{
    private readonly IPilotService _pilotService;
    private readonly PilotConfigItemActions _itemActions;
    public INavigationService NavigationService { get; }
    public ObservableCollection<PilotConfigItemViewModel> Configs { get; set; }

    [ObservableProperty] private PilotConfiguration? _selectedPilotConfiguration;

    public PilotViewModel(INavigationService navigationService, IPilotService pilotService)
    {
        NavigationService = navigationService;
        _pilotService = pilotService;
        _itemActions = new PilotConfigItemActions(SelectConfig, EditConfig, DeleteConfig);
        Configs = [.. pilotService.Configurations.Select(CreateItem)];
    }

    private void SelectConfig(PilotConfigItemViewModel config)
    {
        _pilotService.SelectConfiguration(config.Model);
        foreach (var configItem in Configs)
        {
            configItem.IsSelected = configItem == config;
        }
    }

    private void EditConfig(PilotConfigItemViewModel config)
    {
        NavigationService.TryToNavigate(AppPage.PilotConfig, config.Model);
    }

    private void DeleteConfig(PilotConfigItemViewModel config)
    {
        _pilotService.DeleteConfiguration(config.Model);
        Configs.Remove(config);
    }

    private PilotConfigItemViewModel CreateItem(PilotConfiguration config)
    {
        return new PilotConfigItemViewModel(
            config,
            _itemActions,
            isSelected: config.Id == _pilotService.SelectedConfiguration?.Id
        );
    }
}