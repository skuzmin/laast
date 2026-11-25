using System;
using System.Collections.Generic;

namespace Laast.Client.Models;

public class PilotConfiguration
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<PilotAction> Actions { get; set; } = [];
}