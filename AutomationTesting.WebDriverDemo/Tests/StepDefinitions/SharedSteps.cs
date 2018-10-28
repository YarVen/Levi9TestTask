using AutomationFramework.WEB.Driver;
using AutomationTesting.WebDriverDemo.Helpers;
using TechTalk.SpecFlow;

namespace AutomationTesting.WebDriverDemo.Tests.StepDefinitions
{
    [Binding]
    public class SharedSteps
    {
        public const string CURRENT_OBJECT = "CURRENT_OBJECT";
        public const string CURRENT_AMOUNT = "CURRENT_AMOUNT";

        [Given(@"I have opened (.*) Page")]
        public void GivenIHaveOpenedPage(string pageTitle)
        {
            DriverService.Driver.Navigate().GoToUrl(UrlResolver.GetUrl(pageTitle));
        }
    }
}