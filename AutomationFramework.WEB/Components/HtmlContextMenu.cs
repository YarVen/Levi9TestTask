using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Components
{
  public class HtmlContextMenuItem:HtmlElement
    {
        public HtmlContextMenuItem(SearchConfiguratiuon searchConfig): base(searchConfig)
        {
        }

        public HtmlContextMenuItem(IWebElement webElement): base(webElement)
        {}

        /// <summary>
        /// Check if the context menu is displayed
        /// </summary>
        public override bool Enabled()
        {
            return !NativeElement.GetAttribute("class").Contains("x-menu-item-disabled");
        }
    }
}