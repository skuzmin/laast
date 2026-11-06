using Laast.Enums;

namespace Laast.Models;

public class PilotAction
{
    public PilotActionType? ActionType { get; set; }
    public string? Key { get; set; } = string.Empty;
    public string Description => ActionType is not null ? $"{ActionType}: [{Key}] ({IntervalMinMs}ms-{IntervalMaxMs}ms)" : "";
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public int? DispersionPx { get; set; }
    public int? IntervalMinMs { get; set; }
    public int? IntervalMaxMs { get; set; }

    public PilotAction Clone() => (PilotAction)MemberwiseClone();
}