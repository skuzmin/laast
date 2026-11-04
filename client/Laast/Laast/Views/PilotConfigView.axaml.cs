using Avalonia.Controls;
using Avalonia.Input;

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
}