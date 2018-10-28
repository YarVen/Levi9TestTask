using System;
using OpenQA.Selenium.Chrome;
using DriverService = AutomationFramework.WEB.Driver.DriverService;


namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{
    public static class DialogFactory
    {
        /// <summary>
        /// Return the open file dialog
        /// </summary>
        public static IOpenFileDialog GetOpenFileDialog()
        {
            if (getDriverType()== typeof(ChromeDriver))
                return new ChromeOpenFileDialog();
            throw new NotSupportedException("Your driver doesn't support Open File Dialog");
        }

        /// <summary>
        /// Return the save file dialog
        /// </summary>
        public static ISaveFileDialog GetSaveFileDialog()
        {
            if (getDriverType() == typeof(ChromeDriver))
                return new ChromeSaveFileDialog();
            throw new NotSupportedException("Your driver doesn't support Save File Dialog");
        }

        /// <summary>
        /// Return the WebDriverType
        /// </summary>
        private static Type getDriverType()
        {
            return DriverService.Driver.GetType();
        }

    }
}
