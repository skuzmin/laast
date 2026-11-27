using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Client.Enums;
using Laast.Client.Models;
using Laast.Client.Services;
using Laast.Client.ViewModels.Common;

namespace Laast.Client.ViewModels.Pilot;

public partial class PilotConfigViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IPilotService _pilotService;
    private readonly IConfirmationDialogService _confirmationDialogService;
    private readonly PilotConfiguration _config;
    public ObservableCollection<PilotConfigItemViewModel> Actions { get; }
    private readonly PilotConfigItemContext _itemContext;
    private bool _isBulkUpdate;
    [ObservableProperty] private string _name;
    [ObservableProperty] private bool _isListExpanded;

    public PilotConfigViewModel(
        INavigationService navigationService,
        IConfirmationDialogService confirmationDialogService,
        IPilotService pilotService,
        PilotConfiguration? pilotConfiguration = null)
    {
        pilotConfiguration ??= new PilotConfiguration();
        _name = pilotConfiguration.Name;
        _config = pilotConfiguration;
        _navigationService = navigationService;
        _pilotService = pilotService;
        _confirmationDialogService = confirmationDialogService;
        _itemContext = new PilotConfigItemContext(OnItemExpandedChanged, RemoveAction);
        _isBulkUpdate = false;
        Actions = [.. pilotConfiguration.Actions.Select(item => new PilotConfigItemViewModel(item, _itemContext))];
        OnItemExpandedChanged();
    }

    [RelayCommand]
    private void Back() => _navigationService.TryToNavigate(AppPage.Pilot);

    [RelayCommand]
    private void AddNewAction()
    {
        var newAction = PilotConfigItemViewModel.CreateEmpty(_itemContext);
        newAction.IsExpanded = true;
        Actions.Add(newAction);
        OnItemExpandedChanged();
    }

    [RelayCommand]
    private void ToggleExpandCollapseAll()
    {
        _isBulkUpdate = true;
        foreach (var action in Actions)
        {
            action.IsExpanded = !IsListExpanded;
        }

        _isBulkUpdate = false;
        OnItemExpandedChanged();
    }

    [RelayCommand]
    private void Save()
    {
        _config.Name = Name;
        _config.Actions = [.. Actions.Select(a => a.GetSaveObj())];
        _pilotService.SaveConfiguration(_config);
        _navigationService.TryToNavigate(AppPage.Pilot);
    }

    private void OnItemExpandedChanged()
    {
        if (_isBulkUpdate)
        {
            return;
        }

        IsListExpanded = Actions.All(item => item.IsExpanded);
    }

    private async Task RemoveAction(PilotConfigItemViewModel action)
    {
        var dialogConfig = new ConfirmationDialogConfig()
        {
            Title = "Delete Pilot Action",
            Message = "Are you sure want to delete action ?",
            SubMessage = action.Description,
            ConfirmButtonText = "Delete",
            CancelButtonText = "Cancel"
        };

        if (await _confirmationDialogService.ConfirmAsync(dialogConfig))
        {
            Actions.Remove(action);
            OnItemExpandedChanged();
        }
    }
}