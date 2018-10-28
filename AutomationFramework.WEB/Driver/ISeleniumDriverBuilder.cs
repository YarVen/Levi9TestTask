using System;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Driver
{
    public interface ISeleniumDriverBuilder
    {
        IWebDriver Create(Type type);
    }
}
