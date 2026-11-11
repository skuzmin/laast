using Laast.Client.Models;

namespace Laast.Client.Services;

public interface IAppConfigService
{
    AppConfiguration Configuration { get; }
    void InitConfig();
    void SaveConfig();
}