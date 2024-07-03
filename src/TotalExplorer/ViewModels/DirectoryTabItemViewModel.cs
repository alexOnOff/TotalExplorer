using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using TotalExplorer.ViewModels.FileSystem;

namespace TotalExplorer.ViewModels;

internal class DirectoryTabItemViewModel : ViewModelBase
{
    private FileEntityViewModel? _currentPath;
    private FileEntityViewModel? _selectedFileEntity;

    public DirectoryTabItemViewModel()
    {
        foreach (var disk in Directory.GetLogicalDrives())
        {
            DirectoriesAndFiles.Add(new DirectoryViewModel(disk));
        }

        OpenDirectoryCommand = ReactiveCommand.Create<FileEntityViewModel?>(entity =>
        {
            if (entity is null) return;

            if (entity is DirectoryViewModel directory)
            {
                DirectoriesAndFiles.Clear();
                CurrentDirectory = entity.Name;

                var dirInfo = new DirectoryInfo(entity.FullName);

                foreach (var entry in dirInfo.GetDirectories())
                {
                    DirectoriesAndFiles.Add(new DirectoryViewModel(entry));
                }

                foreach (var entry in dirInfo.GetFiles())
                {
                    DirectoriesAndFiles.Add(new FileViewModel(entry));
                }
            }
        });



        CurrentDirectory = "Home";
    }

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

    #region Commands

    public ReactiveCommand<FileEntityViewModel?, Unit> OpenDirectoryCommand { get; private set; }



    #endregion
}
