using Laast.Models;

namespace Laast.Services;

public interface IAppConfigService
{
    AppConfiguration Configuration { get; }
    void InitConfig();
    void SaveConfig();
}