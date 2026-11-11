using System.Collections.Generic;

namespace Laast.Client.Models;

public class AppConfiguration
{
    public List<PilotConfiguration> PilotConfigurations { get; init; } = [];
}