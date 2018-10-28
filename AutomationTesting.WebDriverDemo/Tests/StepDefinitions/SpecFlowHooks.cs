using AutomationFramework.WEB.Driver;
using TechTalk.SpecFlow;

namespace AutomationTesting.WebDriverDemo.Tests.StepDefinitions
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        [AfterTestRun]
        public static void AfterTestRun()
        {
            DriverService.Quit();
        }
    }
}