using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Components
{
    public class HtmlLabel:HtmlElement
    {
        public HtmlLabel(SearchConfiguratiuon searchConfig): base(searchConfig)
        {
        }

        public HtmlLabel(IWebElement webElement): base(webElement)
        {}

        /// <summary>
        /// Get webElement inner text
        /// </summary>
        public override string GetText()
        {
            var text = NativeElement.Text;

            var parseLabelRegex = new Regex("(\\r\\n)");
            var match = parseLabelRegex.Match(text);

            if (match.Success)
            {
                text = parseLabelRegex.Replace(text, "<===>");
                text = new Regex("(.+?<===>)").Replace(text, "");
            }
            return text;
        }

        /// <summary>
        /// Get the inner text from drop-down string
        /// </summary>
        public string GetDropDownItemText()
        {
            var text = NativeElement.GetAttribute("innerText");
            return text;
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
        /// Get the inner text from all rows of webElement
        /// </summary>
        public string[] GetAllText()
        {
            var text = NativeElement.Text;
            var rows = new List<string>();
            string[] separator = { "<===>" };
            var parseLabelRegex = new Regex("(\\r\\n)");
            var match = parseLabelRegex.Match(text);

            if (match.Success)
            {
                text = parseLabelRegex.Replace(text, "<===>");
                text = text.Replace(",", string.Empty);
                rows = text.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                rows = rows.Select(row => row.Trim()).ToList();
                rows.RemoveAt(0);
                return rows.ToArray();
            }
            rows.Add(text);
            return rows.ToArray();
        }
    }
}
