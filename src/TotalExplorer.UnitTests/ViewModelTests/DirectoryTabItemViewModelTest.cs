using TotalExplorer.ViewModels;
using TotalExplorer.ViewModels.FileSystem;

namespace TotalExplorer.UnitTests.ViewModelTests;

public class DirectoryTabItemViewModelTest
{
    [Fact]
    public void ChangeDirectory_DirectoryShoudBeChanged()
    {
        // Arrange
        var tabItem = new DirectoryTabItemViewModel();
        var newDirInfo = new DirectoryInfo("/Work");
        string? curDirPath = tabItem.CurrentDirectory!.FullName;

        // Act
        tabItem.OpenDirectoryCommand.Execute(new DirectoryViewModel(newDirInfo)).Subscribe();

        // Assert
        Assert.NotEqual(curDirPath, tabItem.CurrentDirectory!.FullName);
    }


}
