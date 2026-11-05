using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace Laast.Views;

public partial class PilotConfigView : UserControl
{
    public PilotConfigView()
    {
        InitializeComponent();
    }

    private void OnHeaderButtonPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;
    }

    private void ToggleAll_OnClick(object? sender, RoutedEventArgs e)
    {
        var expanders = ExpandersContainer.GetVisualDescendants().OfType<Expander>().ToList();
        if (expanders.Count == 0)
        {
            return;
        }

        var anyCollapsed = expanders.Any(x => !x.IsExpanded);
        foreach (var expander in expanders)
        {
            expander.IsExpanded = anyCollapsed;
        }
    }
}