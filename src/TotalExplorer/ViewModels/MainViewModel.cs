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
    private string _newFileName = string.Empty;
    private string _newFileContent = string.Empty;


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

        ToAppDataCommand = ReactiveCommand.Create(() => {
            var dcim = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            SelectedDirectoryTabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(new DirectoryInfo(dcim))).Subscribe();
        });

        ToMusicCommand = ReactiveCommand.Create(() => {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
            SelectedDirectoryTabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(new DirectoryInfo(path))).Subscribe();
        });

        ToDownlandsCommand = ReactiveCommand.Create(() => {
            var dcim = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            SelectedDirectoryTabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(new DirectoryInfo(dcim))).Subscribe();
        });

        CreateFileCommand = ReactiveCommand.Create(() => {
            if(SelectedDirectoryTabItem.CurrentDirectory == null) return;

            var dir = SelectedDirectoryTabItem.CurrentDirectory.FullName;
            var newFile = new FileInfo(Path.Combine(dir, NewFileName));

            if (newFile.Exists) return;

            File.AppendAllText(Path.Combine(dir, NewFileName), NewFileContent);
        });
    }

    public ObservableCollection<DirectoryTabItemViewModel> DirectoryTabItems { get;  set; } = [];

    public DirectoryTabItemViewModel SelectedDirectoryTabItem 
    {
        get => _selectedDirectoryTabItem;
        set => this.RaiseAndSetIfChanged(ref _selectedDirectoryTabItem, value);
    }

    public string NewFileName
    {
        get => _newFileName;
        set => this.RaiseAndSetIfChanged(ref _newFileName, value);
    }

    public string NewFileContent
    {
        get => _newFileContent;
        set => this.RaiseAndSetIfChanged(ref _newFileContent, value);
    }


    public ReactiveCommand<DirectoryTabItemViewModel, Unit> CloseTabCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> AddNewTabCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ToAppDataCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ToMusicCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ToDownlandsCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> CreateFileCommand { get; private set; }
}
