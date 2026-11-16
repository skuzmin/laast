using System.Linq;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.VisualTree;
using Laast.Client.Controls;

namespace Laast.Client.Views.Pilot;

public partial class PilotView : UserControl
{
    public PilotView()
    {
        InitializeComponent();
    }

    private void ConfigsGrid_OnLoadingRow(object? sender, DataGridRowEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var root = e.Row.GetVisualDescendants()
                .OfType<DataGridFrozenGrid>()
                .FirstOrDefault();

            if (root is null || root.Children.OfType<WaveGlow>().Any())
            {
                return;
            }

            var glow = new WaveGlow();
            glow.Bind(
                IsVisibleProperty,
                new Binding("DataContext.IsSelected") { Source = e.Row }
            );
            Grid.SetColumnSpan(glow, root.ColumnDefinitions.Count);
            root.Children.Insert(0, glow);
        });
    }
}