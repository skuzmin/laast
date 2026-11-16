using System;

namespace Laast.Client.ViewModels.Pilot;

public record PilotConfigItemActions(
    Action<PilotConfigItemViewModel> Select,
    Action<PilotConfigItemViewModel> Edit,
    Action<PilotConfigItemViewModel> Delete
);