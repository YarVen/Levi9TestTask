using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Components
{
    public class HtmlButton:HtmlElement
    {
        public HtmlButton(SearchConfiguratiuon searchConfig) : base(searchConfig)
        {
        }

        public HtmlButton(IWebElement webElement) : base(webElement)
        {}

        /// <summary>
        /// Move to the button and perform click
        /// </summary>
        public override void Click()
        {
            Focus();
            base.Click();
        }
    }
}
