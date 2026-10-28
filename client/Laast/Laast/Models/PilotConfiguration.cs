using System;

namespace Laast.Models;

public class PilotConfiguration
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public bool IsSelected { get; set; }
}