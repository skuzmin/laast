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
            new PilotConfiguration { IsActive = false, Name = "Config 1" },
            new PilotConfiguration { IsActive = false, Name = "Config 2" },
            new PilotConfiguration { IsActive = true, Name = "Config 3" }
        ];
    }
}