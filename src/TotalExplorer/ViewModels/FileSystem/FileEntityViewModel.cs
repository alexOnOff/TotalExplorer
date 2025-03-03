namespace TotalExplorer.ViewModels.FileSystem;

public abstract class FileEntityViewModel : ViewModelBase
{
    public string Name { get; }
    public string FullName { get; set; }

    protected FileEntityViewModel(string dirName)
    {
        FullName = dirName;
        Name = dirName;
    }
}
