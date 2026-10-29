using System;
using System.Collections.Generic;
using Laast.Models;

namespace Laast.Services;

public class PilotService : IPilotService
{
    private PilotConfiguration? _selectedPilotConfiguration;

    private readonly List<PilotConfiguration> _configurations =
    [
        new()
        {
            Name =
                "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1"
        },
        new() { Name = "Config 2" },
        new() { Name = "Config 3" }
    ];

    public List<PilotConfiguration> GetPilotConfigurations() => _configurations;

    public void CreatePilotConfiguration(PilotConfiguration configuration)
    {
        _configurations.Add(configuration);
    }

    public void UpdatePilotConfiguration(PilotConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public void SelectPilotConfiguration(PilotConfiguration? configuration)
    {
        _selectedPilotConfiguration = configuration;
    }

    public PilotConfiguration? GetSelectedPilotConfiguration()
    {
        return _selectedPilotConfiguration;
    }

    public void DeletePilotConfiguration(PilotConfiguration configuration)
    {
        _configurations.Remove(configuration);
    }
}