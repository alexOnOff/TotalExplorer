using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using TotalExplorer.Models;
using TotalExplorer.ViewModels.FileSystem;

namespace TotalExplorer.ViewModels;

internal class DirectoryTabItemViewModel : ViewModelBase
{
    private const string HomeStr = "Home";
    private FileEntityViewModel? _currentPath;
    private FileEntityViewModel? _selectedFileEntity;
    private bool _isMoveBackExecutable = false;
    private bool _isMoveForwardExecutable = false;
    private IDirectoryHistory _directoryHistory;

    public DirectoryTabItemViewModel()
    {
        FillCollectionByLogicalDrives();
        _directoryHistory = new DirectoryHistory(HomeStr);

        this.WhenAnyValue(x => x.CurrentDirectory).Subscribe(_ => CheckMoveBack());
        this.WhenAnyValue(x => x.CurrentDirectory).Subscribe(_ => CheckMoveForward());
        var canMoveBack = this.WhenAnyValue(x => x.IsMoveBackExecutable);
        var canMoveForward = this.WhenAnyValue(x => x.IsMoveForwardExecutable);

        OpenDirectoryCommand = ReactiveCommand.Create<FileEntityViewModel?>(entity =>
        {
            if (entity is null) return;

            if (entity is DirectoryViewModel directory)
            {
                DirectoriesAndFiles.Clear();
                _directoryHistory.Add(entity.FullName);
                CurrentDirectory = entity;
                FillCollectionByDirInfo(entity.FullName);
            }
        });

        MoveBackCommand = ReactiveCommand.Create(() => 
        {
            _directoryHistory.MoveBack();
            SetCurrentDirectory();
        }, 
        canMoveBack);

        MoveForwardCommand = ReactiveCommand.Create(() =>
        {
            _directoryHistory.MoveForward();
            SetCurrentDirectory();
        },
        canMoveForward);

        ReloadCommand = ReactiveCommand.Create(SetCurrentDirectory);

        SetCurrentDirectory();
    }

    #region Properties
    public FileEntityViewModel? CurrentDirectory
    {
        get => _currentPath;
        set => this.RaiseAndSetIfChanged(ref _currentPath, value);
    }

    public bool IsMoveBackExecutable
    {
        get => _isMoveBackExecutable;
        set => this.RaiseAndSetIfChanged(ref _isMoveBackExecutable, value);
    }

    public bool IsMoveForwardExecutable
    {
        get => _isMoveForwardExecutable;
        set => this.RaiseAndSetIfChanged(ref _isMoveForwardExecutable, value);
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
    public ReactiveCommand<Unit, Unit> MoveBackCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> MoveForwardCommand {  get; private set; }
    public ReactiveCommand<Unit, Unit> ReloadCommand { get; private set; }

    #endregion

    #region Checks

    private void CheckMoveBack()
    {
        IsMoveBackExecutable = _directoryHistory.CanMoveBack;
    }

    public void CheckMoveForward() 
    {
        IsMoveForwardExecutable = _directoryHistory.CanMoveForward;
    }

    #endregion


    #region Private Methods
    private void FillCollectionByLogicalDrives()
    {
        foreach (var disk in Directory.GetLogicalDrives())
        {
            DirectoriesAndFiles.Add(new DirectoryViewModel(disk));
        }
    }

    private void FillCollectionByDirInfo(string path)
    {
        var dirInfo = new DirectoryInfo(path);

        foreach (var entry in dirInfo.GetDirectories())
        {
            DirectoriesAndFiles.Add(new DirectoryViewModel(entry));
        }

        foreach (var entry in dirInfo.GetFiles())
        {
            DirectoriesAndFiles.Add(new FileViewModel(entry));
        }
    }

    private void SetCurrentDirectory()
    {
        DirectoriesAndFiles.Clear();

        if (_directoryHistory.Current == HomeStr)
        {
            FillCollectionByLogicalDrives();
            CurrentDirectory = new DirectoryViewModel(HomeStr);
        }
        else
        {
            FillCollectionByDirInfo(_directoryHistory.Current);
            CurrentDirectory = new DirectoryViewModel(new DirectoryInfo(_directoryHistory.Current));
        }
    }
    #endregion
}
