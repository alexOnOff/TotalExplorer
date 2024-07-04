using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace TotalExplorer.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private DirectoryTabItemViewModel _selectedDirectoryTabItem;

    public MainViewModel()
    {
        var firstTabItem = new DirectoryTabItemViewModel();
        
        _selectedDirectoryTabItem = firstTabItem;
        DirectoryTabItems.Add(firstTabItem);

        CloseTabCommand = ReactiveCommand.Create< DirectoryTabItemViewModel>(tabItem => 
        {
            if(tabItem.Equals(SelectedDirectoryTabItem)) 
                SelectedDirectoryTabItem = DirectoryTabItems.First();

            DirectoryTabItems.Remove(tabItem);
        });

        AddNewTabCommand = ReactiveCommand.Create(() => {
            DirectoryTabItems.Add(new DirectoryTabItemViewModel());
            SelectedDirectoryTabItem = DirectoryTabItems.Last();
        });
    }

    public ObservableCollection<DirectoryTabItemViewModel> DirectoryTabItems { get;  set; } = [];


    public DirectoryTabItemViewModel SelectedDirectoryTabItem 
    {
        get => _selectedDirectoryTabItem;
        set => this.RaiseAndSetIfChanged(ref _selectedDirectoryTabItem, value);
    }

    public ReactiveCommand<DirectoryTabItemViewModel, Unit> CloseTabCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> AddNewTabCommand { get; private set; }
}
