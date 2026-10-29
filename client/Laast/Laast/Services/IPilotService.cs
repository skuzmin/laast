using System;
using System.Collections.Generic;
using Laast.Models;

namespace Laast.Services;

public interface IPilotService
{
    public List<PilotConfiguration> GetPilotConfigurations();
    public void CreatePilotConfiguration(PilotConfiguration configuration);
    public void UpdatePilotConfiguration(PilotConfiguration configuration);
    public void SelectPilotConfiguration(PilotConfiguration? configuration);
    public PilotConfiguration? GetSelectedPilotConfiguration();
    public void DeletePilotConfiguration(PilotConfiguration? configuration);
}