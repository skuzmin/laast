using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace Laast.Views;

public partial class PilotConfigView : UserControl
{
    private bool _expandNewItem;

    public PilotConfigView()
    {
        InitializeComponent();
        ExpandersContainer.ContainerPrepared += OnContainerPrepared;
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

    private void ExpandNewActionItem_OnClick(object? sender, RoutedEventArgs e) => _expandNewItem = true;

    private void OnContainerPrepared(object? sender, ContainerPreparedEventArgs e)
    {
        if (!_expandNewItem)
        {
            return;
        }

        _expandNewItem = false;

        Dispatcher.UIThread.Post(() =>
        {
            var expander = e.Container as Expander ??
                           e.Container.GetVisualDescendants().OfType<Expander>().FirstOrDefault();

            if (expander is null)
            {
                return;
            }

            expander.IsExpanded = true;
            Dispatcher.UIThread.Post(expander.BringIntoView, DispatcherPriority.Background);
        }, DispatcherPriority.Loaded);
    }
}