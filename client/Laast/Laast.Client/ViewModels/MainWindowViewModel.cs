using CommunityToolkit.Mvvm.Input;
using Laast.Client.Enums;
using Laast.Client.Services;

namespace Laast.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public INavigationService Navigation { get; }

    [RelayCommand]
    private void Navigate(AppPage page) => Navigation.TryToNavigate(page);

    public MainWindowViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        navigation.NavigateToDefaultPage();
    }
}