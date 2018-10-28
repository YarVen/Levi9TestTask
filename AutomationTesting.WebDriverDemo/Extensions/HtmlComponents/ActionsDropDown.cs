using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Driver;
using AutomationFramework.WEB.Extensions;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// XPath example: //button[@type='submit'][@name='objectEventButton']
    /// </summary>
    public class ActionsDropDown : HtmlElement
    {
        private List<HtmlLabel> _itemsFound;
        private List<IWebElement> _emptyList = new List<IWebElement>();
        private List<HtmlLabel> items
        {
            get
            {
                if (_itemsFound == null)
                {
                    ReadOnlyCollection<IWebElement> dropDownItems;
                    try
                    {
                        dropDownItems =
                            NativeElement.FindElements(
                                LocatorTransformer.GetNativeLocators(new FindByAttribute(Method.XPath, "//ul[@class='dropdown-menu buttons-list']/li/button")));
                        _itemsFound = new HtmlElementsCollectionBuilder().BuildElementsCollectionList<HtmlLabel>(dropDownItems);
                    }
                    catch
                    {
                        _itemsFound = new HtmlElementsCollectionBuilder().BuildElementsCollectionList<HtmlLabel>(_emptyList);
                    }
                    _itemsFound.Add(new HtmlLabel(NativeElement));
                }
                return _itemsFound;
            }
        }

        public ActionsDropDown(SearchConfiguratiuon config) : base(config)
        {
        }

        /// <summary>
        /// Check if action drop-down is present
        /// </summary>
        private bool IsDropDownPresent()
        {
            try
            {
                return NativeElement.FindElement(By.XPath("./following-sibling::button")).Displayed;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Return all the items from drop-down list
        /// </summary>
        /// <returns>List of elements</returns>
        public List<HtmlLabel> GetItems()
        {
            return items;
        }

        /// <summary>
        /// Return all the inner text of items from drop-down list
        /// </summary>
        public List<string> GetItemsText()
        {
            if(IsDropDownPresent())
                Expand();
            return items.Select(ddItem => ddItem.GetText().Trim()).ToList();
        }

        /// <summary>
        /// Expand the drop-down list if it isn't opened 
        /// </summary>
        private void Expand()
        {
            if (NativeElement.FindElement(By.XPath("./following-sibling::button")).GetAttribute("aria-expanded") == "false")
            {
                ScrollTo();
                NativeElement.FindElement(By.XPath("./following-sibling::button")).Click();
            }
        }

        /// <summary>
        /// Collapse the drop-down list if it's opened 
        /// </summary>
        private void Collapse()
        {
            if (NativeElement.FindElement(By.XPath("./following-sibling::button")).GetAttribute("aria-expanded") == "true")
                this.ClickOutsideControl();
        }

        /// <summary>
        /// Select an item from drop-down list by given itemName
        /// </summary>
        /// <param name="item">Item name</param>
        public void SelectItem(string item)
        {
            if (IsDropDownPresent())
                Expand();

            if (string.IsNullOrEmpty(item))
            {
                if (IsDropDownPresent())
                    Collapse();
                return;
            }

            var itemToSelect = WaitForItem(item);

            if (itemToSelect == null)
                throw new NoSuchElementException(string.Format("Item with text: '{0}' not found", item));

            itemToSelect.ScrollTo();

            itemToSelect.Click();
        }

        /// <summary>
        /// Wait while the item will be displayed on the drop-down list
        /// </summary>
        /// <param name="item">Item name</param>
        private HtmlLabel WaitForItem(string item)
        {
            DateTime timeout = DateTime.Now + TimeSpan.FromSeconds(10);

            while (timeout > DateTime.Now)
            {
                var itemToSelect = items.FirstOrDefault(ddItem => ddItem.GetText().Trim() == item);
                if (itemToSelect != null)
                    return itemToSelect;
                _itemsFound = null;
                Thread.Sleep(500);
            }
            return null;
        }
    }
}
