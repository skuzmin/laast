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
    private readonly IPilotService _pilotService;
    private readonly PilotConfiguration _config;
    public ObservableCollection<PilotAction> Actions { get; }

    [ObservableProperty] private string _name;
    
    public PilotConfigViewModel(INavigationService navigationService, IPilotService pilotService,
        PilotConfiguration? pilotConfiguration = null)
    {
        pilotConfiguration ??= new PilotConfiguration();
        Actions = [.. pilotConfiguration.Actions.Select(item => item.Clone())];
        _name = pilotConfiguration.Name;
        _config = pilotConfiguration;
        _navigationService = navigationService;
        _pilotService = pilotService;
    }

    [RelayCommand]
    private void Back() => _navigationService.TryToNavigate(AppPage.Pilot);

    [RelayCommand]
    private void AddNewAction() => Actions.Add(new PilotAction());

    [RelayCommand]
    private void RemoveAction(PilotAction action) => Actions.Remove(action);

    [RelayCommand]
    private void Save()
    {
        _config.Name = Name;
        _config.Actions = [.. Actions];
        _pilotService.SaveConfiguration(_config);
        _navigationService.TryToNavigate(AppPage.Pilot);
    }
}