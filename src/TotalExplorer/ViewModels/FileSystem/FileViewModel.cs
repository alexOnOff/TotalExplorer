using System.IO;

namespace TotalExplorer.ViewModels.FileSystem;
public sealed class FileViewModel : FileEntityViewModel
{
    public FileViewModel(string dirName) : base(dirName) 
    {
        FullName = dirName;
    }

    public FileViewModel(FileInfo fileInfo) : base(fileInfo.Name) {

        FullName = fileInfo.FullName;
    }
}
