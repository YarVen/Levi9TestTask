using System.Threading;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;
using DriverService = AutomationFramework.WEB.Driver.DriverService;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// XPath example: //input[contains(@id,'_some_id')]
    /// </summary>
    public class DynamicTextField: HtmlElement
    {
        public DynamicTextField(SearchConfiguratiuon searchConfig) : base(searchConfig)
        {
        }

        public DynamicTextField(IWebElement webElement)
            : base(webElement)
        { }

        /// <summary>
        /// Set text into webElement(field)
        /// </summary>
        public void SetText(string text)
        {
            Click();
            NativeElement.Clear();
            NativeElement.SendKeys(text);
            NativeElement.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Execute click into text field and wait before setting the text
        /// </summary>
        public void SetTextWithWaiting(string text)
        {
            Click();
            Thread.Sleep(250);
            NativeElement.Clear();
            NativeElement.SendKeys(text);
            NativeElement.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Get webElement text
        /// </summary>
        public override string GetText()
        {
            if (!string.IsNullOrEmpty(NativeElement.Text))
                return NativeElement.Text;
            return NativeElement.GetAttribute("value");
        }

        /// <summary>
        /// Set text into webElement(field) by javascript
        /// </summary>
        public void SetTextByJS(string text)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)DriverService.Driver;
            jsExecutor.ExecuteScript("arguments[0].value = '" + text + "';", NativeElement);
            NativeElement.Click();
        }
    }
}
