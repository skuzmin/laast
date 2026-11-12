using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Client.Enums;
using Laast.Client.Models;
using Laast.Client.Services;
using Laast.ProtocolGenerator.Generated;

namespace Laast.Client.ViewModels;

public partial class PilotConfigViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IPilotService _pilotService;
    private readonly PilotConfiguration _config;
    public ObservableCollection<PilotAction> Actions { get; }
    public IReadOnlyList<ActionTypeOption> ActionTypes { get; }
    public IReadOnlyList<Item> MouseButtons { get; }
    public IReadOnlyList<string> KeyboardKeys => LaastProtocol.KeyboardKeys;

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
        ActionTypes =
        [
            new ActionTypeOption(PilotActionType.Keyboard, "Keyboard", "keyboard.png"),
            new ActionTypeOption(PilotActionType.Mouse, "Mouse", "mouse.png"),
        ];
        MouseButtons = 
        [
            new Item(LaastProtocol.MouseButtons.BTN_LEFT, "LMB (Left Mouse Button)"),
            new Item(LaastProtocol.MouseButtons.BTN_RIGHT, "RMB (Right Mouse Button)"),
            new Item(LaastProtocol.MouseButtons.BTN_MIDDLE, "MMB (Middle Mouse Button)"),
            new Item(LaastProtocol.MouseButtons.BTN_FORWARD, "M4 (Mouse Forward Button)"),
            new Item(LaastProtocol.MouseButtons.BTN_BACK, "M5 (Mouse Back Button)"),
        ];
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
        _config.Actions = [.. Actions.Select(a => a.Clone())];
        _pilotService.SaveConfiguration(_config);
        _navigationService.TryToNavigate(AppPage.Pilot);
    }
}