using System.IO;

namespace TotalExplorer.ViewModels.FileSystem;

public sealed class DirectoryViewModel : FileEntityViewModel
{
    public DirectoryViewModel(string dirName) : base(dirName) 
    {
        FullName = dirName;
    }

    public DirectoryViewModel(DirectoryInfo info) : base(info.Name) 
    {
        FullName = info.FullName;
    }
}
