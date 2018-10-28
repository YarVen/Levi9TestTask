using System.Collections.Generic;
using System.Linq;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Extensions;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using DriverService = AutomationFramework.WEB.Driver.DriverService;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// Input for both: adding new chips and choosing the existin
    /// XPath example: //label[text()='Some label']/following-sibling::div//input
    /// </summary>
    public class Chips:HtmlElement
    {
        public Chips(SearchConfiguratiuon config) : base(config)
        { }

        /// <summary>
        /// Select the defined item from chips list by given chips name
        /// </summary>
        /// <param name="text">chips name</param>
        public void AddChipsFromDropDown(string text)
        {
            NativeElement.Click();
            SendKeys(text);
            DriverService.Driver.FindElement(By.XPath(string.Format("//li[text()='{0}']", text))).Click();
        }

        /// <summary>
        /// Delete the first selected chips from input
        /// </summary>
        public void DeleteFirstChips()
        {
            var chipsFound =
                NativeElement.FindElements(
                    LocatorTransformer.GetNativeLocators(new FindByAttribute(Method.XPath,
                        "./../../span"))).FirstOrDefault();
            new HtmlButton(chipsFound.FindElement(By.XPath("./a"))).Click();
            NativeElement.Click();
        }

        /// <summary>
        /// Send keys to CMS chips
        /// </summary>
        private void SendKeys(string text)
        {
            Actions actions = new Actions(DriverService.Driver);
            actions.MoveToElement(NativeElement);
            actions.Click();
            actions.SendKeys(text);
            actions.Build().Perform();
        }

        /// <summary>
        /// Add new records as chips
        /// </summary>
        public void AddNewChips(params string[] chips)
        {
            foreach (var chip in chips)
            {
                SendKeys(chip);
                SendKeys(Keys.Enter);
            }
            NativeElement.ClickOutsideControl();
        }

        /// <summary>
        /// Get the text of each chips
        /// </summary>
        public new List<string> GetText()
        {
            List<string> text = NativeElement.FindElements(By.XPath("./../../span/span")).Select(el=>el.Text.Trim()).ToList();
            return text;
        }
    }
}
