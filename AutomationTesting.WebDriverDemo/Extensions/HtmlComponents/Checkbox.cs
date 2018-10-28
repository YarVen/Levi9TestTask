using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// XPath example: //label[contains(@for, 'Something')]/input[@type='checkbox']
    /// </summary>
    public class Checkbox:HtmlElement
    {
        public Checkbox(SearchConfiguratiuon config) : base(config)
        { }

        public Checkbox(IWebElement webElement): base(webElement)
        { }

        /// <summary>
        /// Check if the checkbox is checked at the moment
        /// </summary>
        public bool IsSelected()
        {
            try
            {
                NativeElement.GetAttribute("checked").Contains("checked");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check the checkbox if it's unchecked
        /// </summary>
        public void Select()
        {
            if (!IsSelected())
                Click();
        }

        /// <summary>
        /// Uncheck the checkbox if it's checked
        /// </summary>
        public void Deselect()
        {
            if (IsSelected())
                Click();
        }

        /// <summary>
        /// Check of uncheck the checkbox based on given bool value and checkbox state
        /// </summary>
        public void Set(bool state)
        {
            if ((!IsSelected() & state) || IsSelected() & !state)
            {
                Click();
            }
                
        }
    }
}
