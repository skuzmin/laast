using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Laast.Client.Enums;
using Laast.Client.Models;
using Laast.Client.Services;
using Laast.Client.ViewModels.Common;

namespace Laast.Client.ViewModels.Pilot;

public partial class PilotViewModel : ViewModelBase
{
    private readonly IPilotService _pilotService;
    private readonly IConfirmationDialogService _confirmationDialogService;
    private readonly PilotConfigItemActions _itemActions;
    public INavigationService NavigationService { get; }
    public ObservableCollection<PilotConfigItemViewModel> Configs { get; set; }

    [ObservableProperty] private PilotConfiguration? _selectedPilotConfiguration;

    public PilotViewModel(
        INavigationService navigationService,
        IPilotService pilotService,
        IConfirmationDialogService confirmationDialogService)
    {
        NavigationService = navigationService;
        _pilotService = pilotService;
        _confirmationDialogService = confirmationDialogService;
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

    private async Task DeleteConfig(PilotConfigItemViewModel config)
    {
        var dialogConfig = new ConfirmationDialogConfig()
        {
            Title = "Delete Pilot Configuration",
            Message = $"Are you sure want to delete [ {config.Model.Name} ] ?",
            ConfirmButtonText = "Delete",
            CancelButtonText = "Cancel"
        };

        if (await _confirmationDialogService.ConfirmAsync(dialogConfig))
        {
            _pilotService.DeleteConfiguration(config.Model);
            Configs.Remove(config);
        }
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