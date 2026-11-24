using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Laast.Client.Helpers;

public static class AppCursors
{
    public static Cursor DefaultCursor { get; } = CreateDefaultCursor();
    public static Cursor TargetCursor { get; } = CreateTargetCursor();

    private static Cursor CreateDefaultCursor()
    {
        var bmp = new Bitmap(AssetLoader.Open(new Uri("avares://Laast.Client/Assets/Cursors/cursor.png")));
        return new Cursor(bmp, new PixelPoint(6, 5));
    }

    private static Cursor CreateTargetCursor()
    {
        var bmp = new Bitmap(AssetLoader.Open(new Uri("avares://Laast.Client/Assets/Cursors/target-cursor.png")));
        var hotspot = new PixelPoint(bmp.PixelSize.Width / 2, bmp.PixelSize.Height / 2);
        return new Cursor(bmp, hotspot);
    }
}