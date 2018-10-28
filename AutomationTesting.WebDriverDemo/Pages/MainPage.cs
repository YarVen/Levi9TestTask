using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using AutomationTesting.WebDriverDemo.Containers;

namespace AutomationTesting.WebDriverDemo.Pages
{
    public class MainPage : Page
    {
        [FindBy(Method.XPath, "//div/h2[contains(text(),'Средний курс валют в банках')]/following-sibling::table/tbody/tr[position()>0]")]
        public HtmlElementsCollection<ExchangeRatesContainer> ExchangeRatesList;

        [FindBy(Method.XPath, "//input[@id='currency_amount']")]
        public HtmlTextField AmountInput;

        [FindBy(Method.XPath, "//p[@id='UAH']/input[@id='currency_exchange']")]
        public HtmlTextField CurrencyExchange;

        [FindBy(Method.XPath, "//li[@id='buy']")]
        public HtmlButton BuyButton;
    }
}