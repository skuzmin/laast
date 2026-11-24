using Avalonia.Interactivity;
using Laast.Client.Controls;
using Laast.Client.Models;

namespace Laast.Client.Views.Dialogs;

public partial class ConfirmationDialog : AppWindow
{
    public ConfirmationDialog()
    {
        InitializeComponent();
    }

    public ConfirmationDialog(ConfirmationDialogConfig config) : this()
    {
        MaxWidth = config.MaxWidth;
        Title = config.Title;
        Message.Text = config.Message;
        SubMessage.Text = config.SubMessage;
        ConfirmButton.Content = config.ConfirmButtonText;
        CancelButton.Content = config.CancelButtonText;
    }

    private void ConfirmButton_OnClick(object? sender, RoutedEventArgs e) => Close(true);
    private void CancelButton_OnClick(object? sender, RoutedEventArgs e) => Close(false);
}