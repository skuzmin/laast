using System;
using System.Collections.Generic;
using Laast.Client.Models;

namespace Laast.Client.Services;

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

    public void SaveConfiguration(PilotConfiguration configuration)
    {
        var index = _appConfigService.Configuration.PilotConfigurations.FindIndex(c => c.Id == configuration.Id);
        if (index == -1)
        {
            _appConfigService.Configuration.PilotConfigurations.Add(configuration);
        }
        else
        {
            _appConfigService.Configuration.PilotConfigurations[index] = configuration;
        }

        _appConfigService.SaveConfig();
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
        _appConfigService.SaveConfig();
    }
}