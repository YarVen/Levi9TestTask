using System;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Search
{
    public static class LocatorTransformer
    {
        /// <summary>
        /// Mapping of custome selectors to selenium's
        /// </summary>
        public static By GetNativeLocators(FindByAttribute findBy)
            
        {
            switch (findBy.Method)
            {
                case Method.Id:
                    return By.Id(findBy.Using);
                case Method.Class:
                    return By.ClassName(findBy.Using);
                case Method.XPath:
                    return By.XPath(findBy.Using);
                case Method.Name:
                    return By.Name(findBy.Using);
                case Method.CssSelector:
                    return By.CssSelector(findBy.Using);
                case Method.LinkText:
                    return By.LinkText(findBy.Using);
                case Method.PartialLinkText:
                    return By.PartialLinkText(findBy.Using);
                case Method.TagName:
                    return By.TagName(findBy.Using);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
