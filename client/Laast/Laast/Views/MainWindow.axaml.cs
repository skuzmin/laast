using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Laast.Controls;
using Laast.ViewModels;
using Microsoft.Extensions.Logging;

namespace Laast.Views;

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
        var bmp = new Bitmap(AssetLoader.Open(new Uri("avares://Laast/Assets/Cursors/cursor.png")));
        RootContainer.Cursor = new Cursor(bmp, new PixelPoint(6, 5));
    }
}