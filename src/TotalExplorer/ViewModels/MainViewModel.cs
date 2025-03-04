using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using TotalExplorer.ViewModels.FileSystem;


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

        ToDcimCommand = ReactiveCommand.Create(() => {
            var dcim = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyComputer);
            SelectedDirectoryTabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(new DirectoryInfo(dcim))).Subscribe();
        });

        ToMusicCommand = ReactiveCommand.Create(() =>
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
            SelectedDirectoryTabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(new DirectoryInfo(path))).Subscribe();
            
        });

        ToDownlandsCommand = ReactiveCommand.Create(() => {
            var dcim = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            SelectedDirectoryTabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(new DirectoryInfo(dcim))).Subscribe();
        });

        CreateFileCommand = ReactiveCommand.Create(() => { 
            
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
    public ReactiveCommand<Unit, Unit> ToDcimCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ToMusicCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ToDownlandsCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> CreateFileCommand { get; private set; }
}
