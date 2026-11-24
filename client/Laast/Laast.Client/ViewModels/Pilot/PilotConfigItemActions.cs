using System;
using System.Threading.Tasks;

namespace Laast.Client.ViewModels.Pilot;

public record PilotConfigItemActions(
    Action<PilotConfigItemViewModel> Select,
    Action<PilotConfigItemViewModel> Edit,
    Func<PilotConfigItemViewModel, Task> Delete
);