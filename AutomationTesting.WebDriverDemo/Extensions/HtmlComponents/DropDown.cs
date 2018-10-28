using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Driver;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// XPath example: //label[text()='Some label']/following-sibling::select
    /// </summary>
    public class DropDown : HtmlElement
    {
        private List<HtmlLabel> _itemsFound;

        private List<HtmlLabel> Items
        {
            get
            {
                if (_itemsFound == null)
                {
                    var dropDownItems = NativeElement.FindElements(By.XPath("./option"));
                    _itemsFound = new HtmlElementsCollectionBuilder().BuildElementsCollectionList<HtmlLabel>(dropDownItems);
                }
                return _itemsFound;
            }
        }

        public DropDown(SearchConfiguratiuon config) : base(config)
        {
        }

        /// <summary>
        /// Retun all the items from drop-down list
        /// </summary>
        /// <returns>List of elements</returns>
        public List<HtmlLabel> GetItems()
        {
            return Items;
        }

        /// <summary>
        /// Select the first appeared ited in expanded list
        /// </summary>
        public void SelectFirstAvailable()
        {
            Expand();
            var items = GetItems().Select(it => it.GetText());
            SelectItem(items.FirstOrDefault());
        }

        /// <summary>
        /// Select an item from drop-down list by given itemName
        /// </summary>
        /// <param name="item">Item name</param>
        public void SelectItem(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return;
            }

            Expand();

            var itemToSelect = WaitForItem(item);

            if (itemToSelect == null)
                throw new NoSuchElementException(string.Format("Item with text: '{0}' not found", item));

            itemToSelect.ScrollTo();

            itemToSelect.Click();
        }

        /// <summary>
        /// Get the first string of element inner text
        /// </summary>
        public string GetFirstText()
        {
            var text = NativeElement.Text;

            var parseLabelRegex = new Regex("(\\r\\n)");
            var match = parseLabelRegex.Match(text);

            if (match.Success)
            {
                text = parseLabelRegex.Replace(text, "<===>");
                text = new Regex("(<===>.+)").Replace(text, "");
            }
            return text;
        }

        /// <summary>
        /// Open the CMS drop-down
        /// </summary>
        public void Expand()
        {
            if (NativeElement.GetAttribute("aria-expanded") == null)
            {
                NativeElement.Click();
                return;
            }               
            if (NativeElement.GetAttribute("aria-expanded").Equals("false"))
            {
                NativeElement.Click();
            }
        }

        /// <summary>
        /// Close the CMS drop-down
        /// </summary>
        public void Collapse()
        {
            if (NativeElement.GetAttribute("aria-expanded") == null)
                return;
            if (NativeElement.GetAttribute("aria-expanded").Equals("true"))
            {
                NativeElement.Click();
            }
        }

        /// <summary>
        /// Wait for item from list to be displayed
        /// </summary>
        /// <param name="item">Item from drop-down list</param>
        private HtmlLabel WaitForItem(string item)
        {
            DateTime timeout = DateTime.Now + TimeSpan.FromSeconds(10);

            while (timeout > DateTime.Now)
            {
                var itemToSelect = Items.FirstOrDefault(ddItem => ddItem.GetText().Trim() == item);
                if (itemToSelect != null)
                    return itemToSelect;
                _itemsFound = null;
                Thread.Sleep(500);
            }
            return null;
        }
    }
}