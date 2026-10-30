using System;
using System.Collections.Generic;

namespace Laast.Models;

public class PilotConfiguration
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<PilotAction> Actions { get; set; } = [];
}