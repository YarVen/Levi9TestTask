using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using DriverService = AutomationFramework.WEB.Driver.DriverService;


namespace AutomationFramework.WEB.Components
{
    public class HtmlElement
    {
        private IWebElement _currentElement;

        /// <summary>
        /// Return the native element(IWebElemen)
        /// </summary>
        public IWebElement NativeElement
        {
            get
            {
                if (_currentElement == null)
                {

                    if (SearchConfig != null)
                    {
                        By searchBy = LocatorTransformer.GetNativeLocators(SearchConfig.FindBy);

                        if (SearchConfig.ParentContainer == null)
                            _currentElement = DriverService.Driver.FindElement(searchBy);
                        else
                            _currentElement =
                                SearchConfig.ParentContainer.NativeElement.FindElement(searchBy);
                    }
                }
                return _currentElement;
            }
            internal set { _currentElement = value; }
        }

        /// <summary>
        /// Set null for current element
        /// </summary>
        public void Refresh()
        {
            Thread.Sleep(250);
            _currentElement = null;
        }

        public HtmlElement()
        {
        }

        public HtmlElement(IWebElement webElement)
        {
            _currentElement = webElement;
        }

        public HtmlElement(SearchConfiguratiuon searchConfig)
        {
            SearchConfig = searchConfig;
        }

        /// <summary>
        /// Check if the webElement is displayed
        /// </summary>
        public bool Displayed
        {
            get
            {
                try
                {
                    return NativeElement.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Remove focus from current webElement
        /// </summary>
        public void LostFocus()
        {
            DriverService.Driver.ExecuteJavaScript("arguments[0].blur();", NativeElement);
        }

        /// <summary>
        /// Take focus of element
        /// </summary>
        public void Focus()
        {
            DriverService.Driver.ExecuteJavaScript("arguments[0].focus();", NativeElement);
        }

        /// <summary>
        /// Execute click
        /// </summary>
        public virtual void Click()
        {
            MoveTo();
            Actions act = new Actions(DriverService.Driver);
            act.Click(NativeElement).Perform();
        }

        public SearchConfiguratiuon SearchConfig { get; set; }

        /// <summary>
        /// Execute drug item to targe webElement
        /// </summary>
        /// <param name="target"></param>
        public void DragItemTo(HtmlElement target)
        {
            Actions builder = new Actions(DriverService.Driver);
            builder.DragAndDrop(NativeElement, target.NativeElement).Build().Perform();
        }

        /// <summary>
        /// Check if webElement is enabled
        /// </summary>
        /// <returns></returns>
        public virtual bool Enabled()
        {
            return NativeElement.Enabled;
        }

        /// <summary>
        /// Execute move to webElement
        /// </summary>
        public void MoveTo()
        {
            Actions act = new Actions(DriverService.Driver);
            act.MoveToElement(NativeElement).Perform();
        }

        /// <summary>
        /// Get webElement inner text
        /// </summary>
        /// <returns></returns>
        public virtual string GetText()
        {
            return NativeElement.Text;
        }

        /// <summary>
        /// Execute right click
        /// </summary>
        public void RightClick()
        {
            Actions builder = new Actions(DriverService.Driver);
            builder.ContextClick(NativeElement).Build().Perform();
        }

        /// <summary>
        /// Try to get webElement
        /// </summary>
        public IWebElement TryGetElement(By by)
        {
            try
            {
                return NativeElement.FindElement(by);
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Try to get webElements
        /// </summary>
        public List<IWebElement> TryGetElements(By by)
        {
            try
            {
                return NativeElement.FindElements(by).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Execute scroll to webElement
        /// </summary>
        public void ScrollTo()
        {
            DriverService.Driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", NativeElement);
        }
    }
}
