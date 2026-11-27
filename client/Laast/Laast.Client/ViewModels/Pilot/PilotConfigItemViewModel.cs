using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Client.Enums;
using Laast.Client.Models;
using Laast.Client.ViewModels.Common;
using Laast.ProtocolGenerator.Generated;

namespace Laast.Client.ViewModels.Pilot;

public partial class PilotConfigItemViewModel(PilotAction model, PilotConfigItemContext context) : ObservableObject
{
    public Field<PilotActionType?> ActionType { get; set; } = Field.Create(model.ActionType);
    public Field<string> Key { get; set; } = Field.Create(model.Key);
    public Field<int?> PositionX { get; set; } = Field.Create(model.PositionX);
    public Field<int?> PositionY { get; set; } = Field.Create(model.PositionY);
    public Field<int?> DispersionPx { get; set; } = Field.Create(model.DispersionPx);
    public Field<int?> IntervalMinMs { get; set; } = Field.Create(model.IntervalMinMs);
    public Field<int?> IntervalMaxMs { get; set; } = Field.Create(model.IntervalMaxMs);
    public string Description { get; set; } = model.Description;

    public IReadOnlyList<ActionTypeOption> ActionTypes { get; } =
    [
        new(PilotActionType.Keyboard, "Keyboard", "keyboard.png"),
        new(PilotActionType.Mouse, "Mouse", "mouse.png"),
    ];

    public IReadOnlyList<Item> MouseButtons { get; } =
    [
        new(LaastProtocol.MouseButtons.BTN_LEFT, "LMB (Left Mouse Button)"),
        new(LaastProtocol.MouseButtons.BTN_RIGHT, "RMB (Right Mouse Button)"),
        new(LaastProtocol.MouseButtons.BTN_MIDDLE, "MMB (Middle Mouse Button)"),
        new(LaastProtocol.MouseButtons.BTN_FORWARD, "M4 (Mouse Forward Button)"),
        new(LaastProtocol.MouseButtons.BTN_BACK, "M5 (Mouse Back Button)"),
    ];

    public IReadOnlyList<string> KeyboardKeys => LaastProtocol.KeyboardKeys;
    [ObservableProperty] private bool _isExpanded;

    partial void OnIsExpandedChanged(bool value)
    {
        context.ExpandedChanged();
    }

    public static PilotConfigItemViewModel CreateEmpty(PilotConfigItemContext context)
        => new(new PilotAction(), context);

    public PilotAction GetSaveObj()
        => new()
        {
            ActionType = ActionType.Value,
            Key = Key.Value,
            PositionX = PositionX.Value,
            PositionY = PositionY.Value,
            IntervalMinMs = IntervalMinMs.Value,
            IntervalMaxMs = IntervalMaxMs.Value,
            DispersionPx = DispersionPx.Value
        };

    [RelayCommand]
    private void SelectClickPosition()
    {
        // _actions.SelectClickPosition();
    }

    [RelayCommand]
    private void RemoveAction() => context.RemoveAction(this);
}