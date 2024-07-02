namespace TotalExplorer.ViewModels.FileSystem;

internal abstract class FileEntityViewModel : ViewModelBase
{
    public string Name { get; }
    protected FileEntityViewModel(string dirName)
    {
        Name = dirName;
    }
}
