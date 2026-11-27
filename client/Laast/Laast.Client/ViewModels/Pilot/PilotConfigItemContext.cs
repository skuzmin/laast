using System;
using System.Threading.Tasks;

namespace Laast.Client.ViewModels.Pilot;

public record PilotConfigItemContext(
    // Action SelectClickPosition,
    Action ExpandedChanged,
    Func<PilotConfigItemViewModel, Task> RemoveAction
);