using OpenQA.Selenium.Appium;
using Xunit;

namespace TestableApp.Appium;

[Collection("Default")]
public class CalculatorTests
{
    private readonly AppiumDriver<AppiumWebElement> _session;


    public CalculatorTests(DefaultAppFixture fixture)
    {
        _session = fixture.Session;
    }

    [Fact]
    public void Should_Add_Numbers()
    {
        var preTitle = _session.FindElementByAccessibilityId("CurDir").Text;
        var element = _session.FindElementByAccessibilityId("MusicMenuItem");

        element.Click();

        Task.Delay(500).Wait();

        Assert.NotEqual(preTitle, _session.FindElementByAccessibilityId("CurDir").Text);
    }
}