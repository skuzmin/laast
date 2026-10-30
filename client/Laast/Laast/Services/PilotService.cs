using System;
using System.Collections.Generic;
using Laast.Models;

namespace Laast.Services;

public class PilotService : IPilotService
{
    private readonly IAppConfigService _appConfigService;
    public IReadOnlyList<PilotConfiguration> Configurations => _appConfigService.Configuration.PilotConfigurations;
    public PilotConfiguration? SelectedConfiguration { get; private set; }

    public PilotService(IAppConfigService appConfigService)
    {
        _appConfigService = appConfigService;
    }

    public void CreateConfiguration(PilotConfiguration configuration)
    {
        _appConfigService.Configuration.PilotConfigurations.Add(configuration);
    }

    public void UpdateConfiguration(PilotConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public void SelectConfiguration(PilotConfiguration configuration)
    {
        SelectedConfiguration = configuration;
    }

    public void DeleteConfiguration(PilotConfiguration configuration)
    {
        if (SelectedConfiguration?.Id == configuration.Id)
        {
            SelectedConfiguration = null;
        }

        _appConfigService.Configuration.PilotConfigurations.Remove(configuration);
    }
}