using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Laast.Client.Controls;
using Laast.Client.ViewModels;
using Microsoft.Extensions.Logging;

namespace Laast.Client.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        InitCursor();
    }

    public MainWindow(MainWindowViewModel vm, ILogger logger) : this()
    {
        DataContext = vm;
        logger.LogInformation("======= App Start =======");
    }

    private void InitCursor()
    {
        var bmp = new Bitmap(AssetLoader.Open(new Uri("avares://Laast.Client/Assets/Cursors/cursor.png")));
        Cursor = new Cursor(bmp, new PixelPoint(6, 5));
    }
}