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
    }

    public MainWindow(MainWindowViewModel vm, ILogger logger) : this()
    {
        DataContext = vm;
        logger.LogInformation("======= App Start =======");
    }
}