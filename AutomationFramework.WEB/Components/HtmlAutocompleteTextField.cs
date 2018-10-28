using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Components
{
    public class HtmlAutocompleteTextField:HtmlElement
    {
        public HtmlAutocompleteTextField(SearchConfiguratiuon searchConfig) : base(searchConfig)
        {
        }

        public HtmlAutocompleteTextField(IWebElement webElement)
            : base(webElement)
        { }

        /// <summary>
        /// Execute set text into webElement
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            NativeElement.SendKeys(text);
        }

        /// <summary>
        /// Set text into webElement (autocomplete field)
        /// </summary>
        /// <param name="text"></param>
        public void Set(string item)
        {
            Click();
            SetText(item);
        }
    }
}
