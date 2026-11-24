using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Laast.Client.Models;
using Laast.Client.Views.Dialogs;

namespace Laast.Client.Services;

public class ConfirmationDialogService : IConfirmationDialogService
{
    public Task<bool> ConfirmAsync(ConfirmationDialogConfig config)
    {
        return Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime
        {
            MainWindow: { } owner
        }
            ? Task.FromResult(false)
            : new ConfirmationDialog(config).ShowDialog<bool>(owner);
    }
}