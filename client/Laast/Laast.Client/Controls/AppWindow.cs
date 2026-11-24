using System;
using Avalonia.Controls;
using Laast.Client.Helpers;

namespace Laast.Client.Controls;

public class AppWindow : Window
{
    protected override Type StyleKeyOverride => typeof(AppWindow);

    protected AppWindow()
    {
        Cursor = AppCursors.DefaultCursor;
    }
}