using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Laast.Client.Models;

namespace Laast.Client.ViewModels.Pilot;

public partial class PilotConfigItemViewModel : ObservableObject
{
    public PilotConfiguration Model { get; }
    public string Name => Model.Name;
    private readonly PilotConfigItemActions _actions;

    [ObservableProperty] private bool _isSelected;

    public PilotConfigItemViewModel(PilotConfiguration model, PilotConfigItemActions actions, bool isSelected)
    {
        Model = model;
        _actions = actions;
        _isSelected = isSelected;
    }

    [RelayCommand]
    private void Select() => _actions.Select(this);

    [RelayCommand]
    private void Edit() => _actions.Edit(this);

    [RelayCommand]
    private void Delete() => _actions.Delete(this);
}