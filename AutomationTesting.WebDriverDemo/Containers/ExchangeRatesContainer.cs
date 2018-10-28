using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;

namespace AutomationTesting.WebDriverDemo.Containers
{
    public class ExchangeRatesContainer : Container
    {
        [FindBy(Method.XPath, "./th")] public HtmlLabel Сurrency;
        [FindBy(Method.XPath, "./td[1]/span/span")] public HtmlLabel Purchase;
        [FindBy(Method.XPath, "./td[2]/span/span")] public HtmlElement Sale;
    }
}