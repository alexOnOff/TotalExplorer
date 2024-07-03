using System.IO;

namespace TotalExplorer.ViewModels.FileSystem;

internal sealed class FileViewModel : FileEntityViewModel
{
    public FileViewModel(string dirName) : base(dirName) 
    {
        FullName = dirName;
    }

    public FileViewModel(FileInfo fileInfo) : base(fileInfo.FullName) {

        FullName = fileInfo.FullName;
    }
}
