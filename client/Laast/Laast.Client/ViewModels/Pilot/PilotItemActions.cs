using System;
using System.Threading.Tasks;

namespace Laast.Client.ViewModels.Pilot;

public record PilotItemActions(
    Action<PilotItemViewModel> Select,
    Action<PilotItemViewModel> Edit,
    Func<PilotItemViewModel, Task> Delete
);