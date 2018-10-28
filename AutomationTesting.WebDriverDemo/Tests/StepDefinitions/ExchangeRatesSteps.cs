using AutomationFramework.WEB.Components;
using AutomationTesting.WebDriverDemo.Pages;
using NUnit.Framework;
using System.Linq;
using AutomationTesting.WebDriverDemo.Containers;
using TechTalk.SpecFlow;

namespace AutomationTesting.WebDriverDemo.Tests.StepDefinitions
{
    [Binding]
    public class ExchangeRatesSteps
    {
        [Given(@"I have wanted to sale (.*) USD")]
        [Given(@"I have wanted to purchase (.*) USD")]
        public void GivenIWantedToSaleUSD(string amount)
        {
            MainPage mainPage = Page.Create<MainPage>();
            mainPage.AmountInput.SetText(amount);
        }

        [Given(@"I have switched to purchasing converter tab")]
        public void GivenIHaveSwitchedToPurchasingConverterTab()
        {
            MainPage mainPage = Page.Create<MainPage>();
            mainPage.BuyButton.Click();
        }

        [When(@"I received current Exchange Rates")]
        public void WhenIReceivedCurrentExchangeRates()
        {
            MainPage mainPage = Page.Create<MainPage>();
            ScenarioContext.Current[SharedSteps.CURRENT_OBJECT] = mainPage.ExchangeRatesList;
        }

        [When(@"I received an amount converted to UAH")]
        public void WhenIReceivedASumConvertedToUAH()
        {
            MainPage mainPage = Page.Create<MainPage>();
            decimal convertedAmount = decimal.Parse(mainPage.CurrencyExchange.GetText().Replace(" ", string.Empty));
            ScenarioContext.Current[SharedSteps.CURRENT_AMOUNT] = convertedAmount;
        }

        [When(@"I tried to set invalid value to the Amount field")]
        public void WhenITriedToSetInvalidValueToSumField()
        {
            MainPage mainPage = Page.Create<MainPage>();
            mainPage.AmountInput.SetText("ababagalamaga");
        }

        [Then(@"I can see purchase is less than selling for each received Exchange Rate")]
        public void ThenICanSeeReceivedExchangeRatesAreCorrect()
        {
            var exchangeRatesList = ScenarioContext.Current.Get<HtmlElementsCollection<ExchangeRatesContainer>>(SharedSteps.CURRENT_OBJECT);

            foreach (var el in exchangeRatesList)
            {
                float sale = float.Parse(el.Sale.GetText());
                float purchase = float.Parse(el.Purchase.GetText());

                Assert.IsTrue(sale > purchase, $"Current purchase rate ({purchase}) should be less than selling rate ({sale}) for {el.Сurrency.GetText()} currency");
            }
        }

        [Then(@"I can see UAH converted seiling amount is correct")]
        public void ThenICanSeeUAHConvertedSaleSumIsCorrect()
        {
            MainPage mainPage = Page.Create<MainPage>();
            decimal receivedConvertedAmount = ScenarioContext.Current.Get<decimal>(SharedSteps.CURRENT_AMOUNT);
            decimal expectedAmount = decimal.Parse(mainPage.ExchangeRatesList.Items.First(el => el.Сurrency.GetText() == "USD").Purchase.GetText()) * 1000;
            Assert.AreEqual(expectedAmount, receivedConvertedAmount);
        }

        [Then(@"I can see UAH converted purchasing amount is correct for (.*) USD")]
        public void ThenICanSeeUAHConvertedPurchSumIsCorrect(int amount)
        {
            MainPage mainPage = Page.Create<MainPage>();
            decimal receivedConvertedAmount = ScenarioContext.Current.Get<decimal>(SharedSteps.CURRENT_AMOUNT);
            decimal expectedAmount = decimal.Parse(mainPage.ExchangeRatesList.Items.First(el => el.Сurrency.GetText() == "USD").Sale.GetText()) * amount;
            Assert.AreEqual(expectedAmount, receivedConvertedAmount);
        }

        [Then(@"I can see the Amount field is empty")]
        public void ThenICanSeeSumFieldIsEmpty()
        {
            MainPage mainPage = Page.Create<MainPage>();
            Assert.IsEmpty(mainPage.AmountInput.GetText(), "Sum field should be empty after attempt to set invalid value");
        }
    }
}