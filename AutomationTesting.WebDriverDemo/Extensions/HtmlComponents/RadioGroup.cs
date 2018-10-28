using System;
using System.Collections.Generic;
using System.Linq;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationTesting.WebDriverDemo.Extensions.HtmlComponents
{
    /// <summary>
    /// XPath example: //div[@id='some_id']
    /// </summary>
    public class RadioGroup : HtmlElement
    {
        public RadioGroup(SearchConfiguratiuon config) : base(config)
        { }

        private List<HtmlElement> _buttons;

        public List<HtmlElement> Buttons
        {

            get
            {
                if (_buttons == null)
                {
                    var elements = NativeElement.FindElements(By.XPath(".//label")).ToList();

                    if (elements.Any())
                    {
                        _buttons = new List<HtmlElement>();
                        elements.ForEach(native => _buttons.Add(new HtmlElement(native)));
                    }
                }
                return _buttons;
            }
        }

        /// <summary>
        /// Return the checked item name
        /// </summary>
        public string GetCheckedButtonName()
        {
            var checkedButton = Buttons.Select(button => button.NativeElement)
                .FirstOrDefault(nat => nat.GetAttribute("checked").Contains("checked"));

            return checkedButton?.Text;
        }

        /// <summary>
        /// Execute checking the appropriate point by provided name
        /// </summary>
        /// <param name="buttonName">Name of item to select</param>
        public void Set(string buttonName)
        {
            var buttons = NativeElement.FindElements(By.XPath(".//label"));
            var button = buttons.FirstOrDefault(native => native.Text.Trim().ToLower().Equals(buttonName.Trim().ToLower()));

            if (button == null)
                throw new ApplicationException(buttonName + " not found");

            var rButton = new HtmlElement(button);
            rButton.Focus();
            rButton.Click();
        }
    }
}