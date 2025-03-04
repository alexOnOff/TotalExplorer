using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace TotalExplorer.UITests;

public class ChangeDirTest : BaseTest
{
    [Test]
    public void ChangeDir()
    {
        //https://github.com/AvaloniaUI/Avalonia/tree/master/tests/Avalonia.IntegrationTests.Appium
        //https://github.com/AvaloniaUI/Avalonia/discussions/8513
        var window = App.FindElement(By.TagName("win"));

        var title = FindUIElement("CurDir").Text;
        var element = FindUIElement("MusicMenuItem");
        element.Click();
        Task.Delay(500).Wait();

        Assert.That(title, Is.Not.EqualTo(FindUIElement("CurDir").Text));
    }
}
