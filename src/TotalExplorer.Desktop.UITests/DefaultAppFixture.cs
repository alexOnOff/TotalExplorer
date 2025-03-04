using System.Globalization;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Mac;
using OpenQA.Selenium.Appium.Windows;
using Xunit;

namespace TestableApp.Appium;

[CollectionDefinition("Default")]
public class DefaultCollection : ICollectionFixture<DefaultAppFixture>
{
}


public class DefaultAppFixture : IDisposable
{
    private const string TestAppPath =
        @"E:\Work\AvaloniaProjects\TotalExplorer\src\TotalExplorer.Desktop\bin\Debug\net7.0-windows10.0.17763.0\TotalExplorer.Desktop.exe";

    private const string TestAppBundleId = "net.avaloniaui.testableApp";

    public DefaultAppFixture()
    {
        var options = new AppiumOptions();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            ConfigureWin32Options(options);
            Session = new WindowsDriver<AppiumWebElement>(
                new Uri("http://127.0.0.1:4723"),
                options);

            // https://github.com/microsoft/WinAppDriver/issues/1025
            SetForegroundWindow(new IntPtr(int.Parse(
                Session.WindowHandles[0].Substring(2),
                NumberStyles.AllowHexSpecifier)));
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            ConfigureMacOptions(options);
            Session = new MacDriver<AppiumWebElement>(
                new Uri("http://127.0.0.1:4723/wd/hub"),
                options);
        }
        else
        {
            throw new NotSupportedException("Unsupported platform.");
        }
    }

    protected virtual void ConfigureWin32Options(AppiumOptions options)
    {
        var path = Path.GetFullPath(TestAppPath);
        options.AddAdditionalCapability("appium:automationName", "windows");

        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Windows);
        options.AddAdditionalCapability("appium:app", path);
        //options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

        // options.AddAdditionalCapability("appArguments", "--customArg");
    }

    protected virtual void ConfigureMacOptions(AppiumOptions options)
    {
        options.AddAdditionalCapability("appium:bundleId", TestAppBundleId);
        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.MacOS);
        options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "mac2");
        options.AddAdditionalCapability("appium:showServerLogs", true);
        // options.AddAdditionalCapability("appium:arguments", new[] { "--customArg" });
    }

    public AppiumDriver<AppiumWebElement> Session { get; }

    public void Dispose()
    {
        try
        {
            Session.Close();
        }
        catch
        {
            // Closing the session currently seems to crash the mac2 driver.
        }
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
}
