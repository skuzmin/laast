using CommunityToolkit.Mvvm.ComponentModel;

namespace Laast.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentPage;
    public MainWindowViewModel()
    {
        CurrentPage = new HomeViewModel();
    }
}