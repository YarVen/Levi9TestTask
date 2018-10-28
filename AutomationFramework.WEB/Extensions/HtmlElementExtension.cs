using System;
using System.Threading;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using DriverService = AutomationFramework.WEB.Driver.DriverService;

namespace AutomationFramework.WEB.Extensions
{
    public static class HtmlElementExtension
    {
        /// <summary>
        /// Wait for element to be displayed
        /// </summary>
        public static void WaitForDisplayed(this HtmlElement element)
        {
            WebDriverWait wait = new WebDriverWait(DriverService.Driver, TimeSpan.FromSeconds(15));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException), typeof(NoSuchElementException));
            wait.Until(
                ExpectedConditions.ElementIsVisible(LocatorTransformer.GetNativeLocators(element.SearchConfig.FindBy)));
            wait.PollingInterval = TimeSpan.FromSeconds(2);          
        }

        /// <summary>
        /// Wait while element to be invisible or gone
        /// </summary>
        public static void WaitWhileElementToBeInvisible(this HtmlElement element)
        {
            var timer = DateTime.Now.AddSeconds(15);
            if (element.Displayed)
            {
                try
                {
                    while (element.Displayed)
                    {
                        if (timer < DateTime.Now)
                            throw new ArgumentOutOfRangeException();
                        Thread.Sleep(333);
                        element.Refresh();
                    }
                }
                catch (NoSuchElementException)
                {
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException(nameof(element), $"The element: {element.NativeElement} is still displayed");
                }
            }
        }

        /// <summary>
        /// Check if element present/active (active, means that the element doesn't have StaleElementReferenceException())
        /// </summary>
        public static bool IsElementDisplayed(this HtmlElement element)
        {
            try
            {
                return element.NativeElement.Displayed;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Execute Click outside the control (elsewhere)
        /// </summary>
        public static void ClickOutsideControl(this HtmlElement element)
        {
            Actions builder = new Actions(DriverService.Driver);
            builder.MoveToElement(element.NativeElement, -50,-50).Click().Perform();
        }

        /// <summary>
        /// Execute Click outside the control (elsewhere)
        /// </summary>
        public static void ClickOutsideControl(this IWebElement element)
        {
            Actions builder = new Actions(DriverService.Driver);
            builder.MoveToElement(element, -50, -50).Click().Perform();
        }
    }

}
