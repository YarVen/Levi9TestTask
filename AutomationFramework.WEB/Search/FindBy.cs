using System;

namespace AutomationFramework.WEB.Search
{
    public class FindByAttribute:Attribute
    {
        public readonly Method Method;
        public readonly string Using;

        /// <summary>
        /// Custome attribute
        /// </summary>
        /// <param name="method">Value from enum</param>
        /// <param name="value">String</param>
        public FindByAttribute(Method method, string value)
        {
            Method = method;
            Using = value;
        }
    }
}
