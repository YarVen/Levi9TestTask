using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AutomationFramework.WEB.Driver
{
    public class BrowserPidIdentifier
    {
        private readonly Type _browserType;
        private List<int> _pidsBeforeSeleniumStarted;


        public BrowserPidIdentifier(Type browserType)
        {
            _browserType = browserType;
        }


        public void TakeInitialState()
        {
            _pidsBeforeSeleniumStarted=getPids(_browserType);
        }

        public Process GetCurrentBrowserPid()
        {
            List<int> pidsAfter = getPids(_browserType);
            return Process.GetProcessById(pidsAfter.Except(_pidsBeforeSeleniumStarted).LastOrDefault());
        }


        private List<int> getPids(Type browserType)
        {
            if (browserType == typeof(FirefoxDriver))
                return Process.GetProcessesByName("firefox").Select(p => p.Id).ToList();

            if (browserType == typeof (ChromeDriver))
                return
                    Process.GetProcessesByName("chrome").Where(p => p.MainWindowHandle != IntPtr.Zero).Select(pr => pr.Id)
                        .ToList();

            throw new NotImplementedException("Not implemented for :"+ browserType);
        } 
    }
}
