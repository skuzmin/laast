using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Laast.Controls;

public partial class TitleBar : UserControl
{
    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<TitleBar, string?>(nameof(Title));

    public static readonly StyledProperty<IImage?> IconProperty =
        AvaloniaProperty.Register<TitleBar, IImage?>(nameof(Icon));

    public static readonly StyledProperty<bool> IsMinimizeAvailableProperty =
        AvaloniaProperty.Register<TitleBar, bool>(nameof(IsMinimizeAvailable));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public IImage? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsMinimizeAvailable
    {
        get => GetValue(IsMinimizeAvailableProperty);
        set => SetValue(IsMinimizeAvailableProperty, value);
    }

    public TitleBar()
    {
        InitializeComponent();
    }

    private Window? Host => TopLevel.GetTopLevel(this) as Window;

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            Host?.BeginMoveDrag(e);
    }

    private void Minimize_Click(object? sender, RoutedEventArgs e)
    {
        if (Host is { } w) w.WindowState = WindowState.Minimized;
    }

    private void Close_Click(object? sender, RoutedEventArgs e) => Host?.Close();
}