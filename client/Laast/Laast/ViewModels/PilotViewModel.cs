using System.Collections.ObjectModel;
using Laast.Models;
using Laast.Services;

namespace Laast.ViewModels;

public partial class PilotViewModel : ViewModelBase
{
    public ObservableCollection<PilotConfiguration> Configs { get; set; }
    public INavigationService NavigationService { get; }

    public PilotViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        Configs =
        [
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = false, Name = "Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1Config 1" },
            new PilotConfiguration { IsSelected = false, Name = "Config 2" },
            new PilotConfiguration { IsSelected = true, Name = "Config 3" }
        ];
    }
}