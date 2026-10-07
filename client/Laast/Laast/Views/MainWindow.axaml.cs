using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Laast.Controls;

namespace Laast.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        InitCursor();
    }

    private void InitCursor()
    {
        var bmp = new Bitmap(AssetLoader.Open(new Uri("avares://Laast/Assets/Cursors/cursor.png")));
        RootContainer.Cursor = new Cursor(bmp, new PixelPoint(6, 5));
    }
}