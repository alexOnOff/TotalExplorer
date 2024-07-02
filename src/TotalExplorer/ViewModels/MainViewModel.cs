using ReactiveUI;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using TotalExplorer.ViewModels.FileSystem;

namespace TotalExplorer.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private FileEntityViewModel? _currentPath;
    private FileEntityViewModel? _selectedFileEntity;

    public MainViewModel()
    {

        foreach (var disk in Directory.GetLogicalDrives()) 
        {
            DirectoriesAndFiles.Add(new DirectoryViewModel(disk));
        }


        OpenDirectoryCommand = ReactiveCommand.Create<FileEntityViewModel?>(entity =>
        {
            if (entity is null) return;

            DirectoriesAndFiles.Clear();

            foreach (var entry in Directory.EnumerateDirectories(entity.Name))
            {
                DirectoriesAndFiles.Add(new DirectoryViewModel(entry));
            }

            foreach (var entry in Directory.EnumerateFiles(entity.Name))
            {
                DirectoriesAndFiles.Add(new FileViewModel(entry));
            }
        });
    }

    #region Commands

    public ReactiveCommand<FileEntityViewModel?, Unit> OpenDirectoryCommand { get; private set; } 

    #endregion


    #region Properties
    public string CurrentDirectory
    {
        get => _currentPath is not null ? _currentPath.Name : string.Empty;
        set => this.RaiseAndSetIfChanged(ref _currentPath, new DirectoryViewModel(value));
    }

    public FileEntityViewModel? SelectedFileEntity
    {
        get => _selectedFileEntity;
        set => this.RaiseAndSetIfChanged(ref _selectedFileEntity, value);
    }


    public ObservableCollection<FileEntityViewModel> DirectoriesAndFiles { get; set; } = new();
    #endregion

}
