using System.Collections.Generic;
using System.Linq;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using DriverService = AutomationFramework.WEB.Driver.DriverService;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// List of stable chips, only available for choosing 
    /// XPath example: //label[text()='Some label']/following-sibling::span
    /// </summary>
    public class ChipsList : HtmlElement
    {
        public string CurrentSelectedChips;

        public ChipsList(SearchConfiguratiuon config) : base(config)
        { }

        /// <summary>
        /// Select the defined item from chips list by given chips name
        /// </summary>
        /// <param name="text">chips name</param>
        public void AddChipsFromDropDown(string text)
        {
            NativeElement.Click();
            SendKeys(text);
            DriverService.Driver.FindElement(By.XPath($"//ul[contains(@class, 'select')]/li[contains(text(), '{text}')]")).Click();
        }

        /// <summary>
        /// Delete the first selected chips from input
        /// </summary>
        public void DeleteFirstChips()
        {
            var chipsFound =
                NativeElement.FindElements(
                    LocatorTransformer.GetNativeLocators(new FindByAttribute(Method.XPath,
                        "../..//li[@title!='']"))).FirstOrDefault();
            new HtmlButton(chipsFound.FindElement(By.XPath(".//span"))).Click();
            NativeElement.Click();
        }

        /// <summary>
        /// Send keys to CMS chips
        /// </summary>
        private void SendKeys(string text)
        {
            Actions actions = new Actions(DriverService.Driver);
            actions.MoveToElement(NativeElement);
            actions.SendKeys(text);
            actions.Build().Perform();
        }

        /// <summary>
        /// Retun all the items names from chips list
        /// </summary>
        public List<string> GetAvailableValues()
        {
            NativeElement.Click();
            var elementList = DriverService.Driver.FindElements(By.XPath("//li[contains(@id, '-result-')]"));
            var receivedList = elementList.Select(element => element.Text).ToList();
            NativeElement.Click();
            return receivedList;
        }

        /// <summary>
        /// Get text of the first selected chips element
        /// </summary>
        /// <returns>Attibute title</returns>
        public override string GetText()
        {
            string text = NativeElement.FindElement(By.XPath(".//li[1]")).GetAttribute("Title");
            return text;
        }
    }
}