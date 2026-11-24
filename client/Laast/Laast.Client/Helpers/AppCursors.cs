using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Laast.Client.Helpers;

public static class AppCursors
{
    public static Cursor DefaultCursor { get; } = Create();

    private static Cursor Create()
    {
        var bmp = new Bitmap(AssetLoader.Open(new Uri("avares://Laast.Client/Assets/Cursors/cursor.png")));
        return new Cursor(bmp, new PixelPoint(6, 5));
    }
}