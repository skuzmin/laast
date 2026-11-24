using System.Threading.Tasks;
using Laast.Client.Models;

namespace Laast.Client.Services;

public interface IConfirmationDialogService
{
    Task<bool> ConfirmAsync(ConfirmationDialogConfig config);
}