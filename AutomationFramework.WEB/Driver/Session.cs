using System;
using System.Configuration;
using System.Diagnostics;
using AutomationFramework.WEB.FrameworkConfig;
using Ninject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace AutomationFramework.WEB.Driver
{
    public class Session
    {
        public Process BrowserProcess
        {
            get
            {
                _browserProcess.Refresh();
                return _browserProcess;
            }
            private set { _browserProcess = value; }
        }

        Type _webDriverType;
        private Process _browserProcess;

        /// <summary>
        /// Add ninject dependencies
        /// </summary>
        private void initializeDependencies()
        {
            foreach (Dependency dependency in DependencySectionHandler.GetConfig().Dependencies)
               DependencyManager.AddDependency(dependency.Interface, dependency.Implementation);    
        }

        /// <summary>
        /// Choose appropriate browser driver
        /// </summary>
        private void initializeAppSettings()
        {
            switch (ConfigurationManager.AppSettings["Browser"])
            {
                case "IE":               
                    _webDriverType = typeof(InternetExplorerDriver);
                    break;
                
                case "Chrome":
                    _webDriverType = typeof(ChromeDriver);
                    break;

                case "FF":
                    _webDriverType = typeof(FirefoxDriver);
                    break;
            }
        }

        /// <summary>
        /// Initialize web session
        /// </summary>
        public Session()
        {
            initializeAppSettings();
            initializeDependencies();
            BrowserPidIdentifier identifier=new BrowserPidIdentifier(_webDriverType);
            identifier.TakeInitialState();
            IWebDriver driver = DependencyManager.Kernel.Get<ISeleniumDriverBuilder>().Create(_webDriverType);
            BrowserProcess = identifier.GetCurrentBrowserPid();
            Current = driver;
        }

        public IWebDriver Current { private set; get; }
    }
}
