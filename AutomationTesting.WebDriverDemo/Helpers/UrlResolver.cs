using System;

namespace AutomationTesting.WebDriverDemo.Helpers
{
    public static class UrlResolver
    {
        public static string GetUrl(string pageTitle)
        {
            switch (pageTitle.ToLower().Trim())
            {
                case "main":
                    return Constants.RootURL;
                default:
                    throw new ArgumentException($"Incorrect page title detected: {pageTitle.ToUpper()}");
            }
        }
    }
}