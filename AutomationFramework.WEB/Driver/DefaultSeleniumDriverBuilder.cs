using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace AutomationFramework.WEB.Driver
{
    public class DefaultSeleniumDriverBuilder : ISeleniumDriverBuilder
    {
        /// <summary>
        /// Set appropriate driver with options
        /// </summary>
        public IWebDriver Create(Type type)
        {
            if (type == typeof(ChromeDriver))
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("test-type");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddArguments("--start-maximized");
                var driver= new ChromeDriver(options);
                driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));
                return driver;
            }

            if (type == typeof(InternetExplorerDriver))
            {
                InternetExplorerOptions options = new InternetExplorerOptions();
                options.EnableNativeEvents = true;
                options.RequireWindowFocus = true;
                options.IgnoreZoomLevel = true;
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                return new InternetExplorerDriver(options);
            }

            if (type == typeof(FirefoxDriver))
            {
                return new FirefoxDriver();
            }

            throw new NotImplementedException();
        }
    }
}
