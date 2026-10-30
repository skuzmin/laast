using System;
using Laast.Enums;

namespace Laast.Models;

public class PilotAction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public PilotActionType? ActionType { get; set; }
    public string Key { get; set; } = string.Empty;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int DispersionPx { get; set; }
    public PilotRepeatMode? RepeatMode { get; set; }
    public int IntervalMinMs { get; set; }
    public int IntervalMaxMs { get; set; }
    public int RepeatsCount { get; set; }
}