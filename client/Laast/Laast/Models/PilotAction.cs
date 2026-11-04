using System;
using Laast.Enums;

namespace Laast.Models;

public class PilotAction
{
    private Guid Id { get; init; } = Guid.NewGuid();
    public PilotActionType? ActionType { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Description { get; set; } = "DESCRIPTION_TEXT";
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int DispersionPx { get; set; }
    public int IntervalMinMs { get; set; }
    public int IntervalMaxMs { get; set; }

    public PilotAction Clone() => (PilotAction)MemberwiseClone();
}