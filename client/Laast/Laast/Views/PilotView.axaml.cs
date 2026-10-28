using Avalonia.Controls;

namespace Laast.Views;

public partial class PilotView : UserControl
{
    public PilotView()
    {
        InitializeComponent();
    }

    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid { SelectedItem: not null } grid)
        {
            grid.SelectedItem = null;
        }
    }
}