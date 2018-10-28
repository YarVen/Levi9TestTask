using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Driver
{
    public static class DriverService
    {

        private static  IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = SessionService.Session.Current;
                    return _driver;
                }

                if (!IsDriverAlive())
                    OpenNewWindowAndSwitch();

                return _driver;
            }
        }

        public static TDriverType Get<TDriverType>()
        {
            return (TDriverType)SessionService.Session.Current;
        }

        /// <summary>
        /// Execute closing browser window
        /// </summary>
        public static void CloseCurrentWindow()
        {
            if (_driver != null)
            {
                string currentWindowHandle = _driver.CurrentWindowHandle;
                OpenNewWindowAndSwitch();
                _driver.SwitchTo().Window(currentWindowHandle).Close();
                _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            }
        }

        /// <summary>
        /// Close the current tab and back to the previous
        /// </summary>
        public static void BackToThePreviousWindow()
        {
            if (_driver != null)
            {

                List<string> currentTabs = _driver.WindowHandles.ToList();
                _driver.Close();
                _driver.SwitchTo().Window(currentTabs[0]);
            }
        }

        /// <summary>
        /// Clear browser cookies and cache
        /// </summary>
        public static void Clear()
        {
           _driver.Manage().Cookies.DeleteAllCookies();
            ClearLocalStorage();
        }


        /// <summary>
        /// Execute quit from browser
        /// </summary>
        public static void Quit()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        /// <summary>
        /// Check if driver isn't closed
        /// </summary>
        public static bool IsDriverAlive()
        {
            try
            {              
                if (String.IsNullOrEmpty(_driver.CurrentWindowHandle))
                return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Open new browser window and switch on it
        /// </summary>
        public static string OpenNewWindowAndSwitch()
        {
            IJavaScriptExecutor jscript = _driver as IJavaScriptExecutor;
            jscript.ExecuteScript("window.open()");
            string newWindowHandle = _driver.WindowHandles.Last();
            _driver.SwitchTo().Window(newWindowHandle);
            return newWindowHandle;
        }

        /// <summary>
        /// Take the browser page screenshot
        /// </summary>
        public static string TakeScreenshot(string file)
        {
            Screenshot shot = ((ITakesScreenshot)Driver).GetScreenshot();
            var fileToSave = GetPathToFileinAssemblyDirectory(file);

            var dir = new FileInfo(fileToSave).Directory;
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);

            shot.SaveAsFile(fileToSave, ImageFormat.Png);
            return fileToSave;
        }

        /// <summary>
        /// Get full path to the file in assembly directory
        /// </summary>
        public static string GetPathToFileinAssemblyDirectory(string fileName)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.Combine(Path.GetDirectoryName(path), fileName);
        }

        /// <summary>
        /// Cleare browser cache
        /// </summary>
        public static void ClearLocalStorage()
        {
            IJavaScriptExecutor jscript = _driver as IJavaScriptExecutor;
            jscript.ExecuteScript("localStorage.clear()");
        }
    }
}