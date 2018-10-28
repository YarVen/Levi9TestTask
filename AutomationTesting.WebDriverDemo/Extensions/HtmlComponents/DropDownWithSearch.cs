using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Driver;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// XPath example: //label[text()='Some label']/following-sibling::span/span/span[1]
    /// </summary>
    public class DropDownWithSearch : HtmlElement
    {
        private List<HtmlLabel> _itemsFound;
        private List<HtmlLabel> Items
        {
            get
            {
                if (_itemsFound == null)
                {
                    var dropDownItems = 
                        NativeElement.FindElements(LocatorTransformer.GetNativeLocators(new FindByAttribute(Method.XPath, "//span[@class='select2-results']/ul/li[count(text()[contains(., 'Choose')]) = 0]")));
                    _itemsFound = new HtmlElementsCollectionBuilder().BuildElementsCollectionList<HtmlLabel>(dropDownItems);
                }
                return _itemsFound;
            }
        }

        public DropDownWithSearch(SearchConfiguratiuon config) : base(config)
        {
        }

        /// <summary>
        /// Retun all the items from drop-down list
        /// </summary>
        /// <returns>List of elements</returns>
        public List<HtmlLabel> GetItems()
        {
            Expand();
            return Items;
        }

        /// <summary>
        /// Expand the drop-down list if it isn't opened 
        /// </summary>
        public void Expand()
        {
            if (NativeElement.GetAttribute("aria-expanded") == "false")
            {
                Click();
            }
        }

        /// <summary>
        /// Return the error message of drop-down list
        /// </summary>
        public string GetErrorMessage()
        {
            var element = TryGetElement(By.XPath("../div/div"));
            return element?.Text;
        }

        private HtmlLabel WaitForItem(string item)
        {
            DateTime timeout = DateTime.Now + TimeSpan.FromSeconds(10);

            while (timeout > DateTime.Now)
            {
                var itemToSelect = Items.FirstOrDefault(ddItem => ddItem.NativeElement.GetAttribute("innerText") == item);
                if (itemToSelect != null)
                    return itemToSelect;
                _itemsFound = null;
                Thread.Sleep(500);
            }
            return null;
        }

        /// <summary>
        /// Return the text of selected element in drop-down list
        /// </summary>
        public override string GetText()
        {
            var selectedElement = TryGetElement(By.XPath("./span"));

            if (selectedElement == null)
                return string.Empty;

            return selectedElement.Text;
        }
    }
}