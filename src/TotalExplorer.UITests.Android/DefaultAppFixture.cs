using System.Globalization;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
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
        @"E:\Work\AvaloniaProjects\TotalExplorer\src\TotalExplorer.Android\bin\Debug\net7.0-android\com.CompanyName.AvaloniaTest-Signed.apk";

    private const string TestAppBundleId = "net.avaloniaui.testableApp";

    public DefaultAppFixture()
    {
        var options = new AppiumOptions();

        ConfigureAndroidOptions2(options);
        Session = new AndroidDriver<AppiumWebElement>(new Uri("http://127.0.0.1:4723"),
                options);
    }

    protected virtual void ConfigureAndroidOptions(AppiumOptions options)
    {
        var path = Path.GetFullPath(TestAppPath);
        options.AddAdditionalCapability("appium:automationName", "UIAutomator2");
        options.AddAdditionalCapability(MobileCapabilityType.NoReset, "true");
        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
        options.AddAdditionalCapability("appium:app", path);
        options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, $"com.CompanyName.AvaloniaTest.MainActivity");
        //options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
        options.AddAdditionalCapability("avd", "pixel_5_-_api_33");
        // options.AddAdditionalCapability("appArguments", "--customArg");
    }

    protected virtual void ConfigureAndroidOptions2(AppiumOptions options)
    {
        var path = Path.GetFullPath(TestAppPath);
        
        options.AddAdditionalCapability("appium:automationName", "UIAutomator2");
        options.AddAdditionalCapability(MobileCapabilityType.NoReset, "true");
        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
        options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.CompanyName.AvaloniaTest");
        options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, $"com.CompanyName.AvaloniaTest.MainActivity");
        //options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
        options.AddAdditionalCapability(AndroidMobileCapabilityType.Avd, "pixel_5_-_api_33");
        // options.AddAdditionalCapability("appArguments", "--customArg");
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

   
}
