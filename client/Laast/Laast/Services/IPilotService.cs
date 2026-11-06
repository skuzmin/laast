using System.Collections.Generic;
using Laast.Models;

namespace Laast.Services;

public interface IPilotService
{
    PilotConfiguration? SelectedConfiguration { get; }
    IReadOnlyList<PilotConfiguration> Configurations { get; }
    void CreateConfiguration(PilotConfiguration configuration);
    void SaveConfiguration(PilotConfiguration configuration);
    void SelectConfiguration(PilotConfiguration configuration);
    void DeleteConfiguration(PilotConfiguration configuration);
}