using System;
using AutomationFramework.WEB.Driver;
using AutomationFramework.WEB.Search;

namespace AutomationFramework.WEB.Components
{
    public abstract class Page 
    {
        public Page()
        {}

        /// <summary>
        /// Create the instance of page with access to page elements
        /// </summary>
        /// <typeparam name="TPagetype">PageType</typeparam>
        public static TPagetype Create<TPagetype>() where TPagetype : Page
        {
            TPagetype instance = Activator.CreateInstance<TPagetype>();
            new PageBuilder().Build(instance);
            return instance;
        }

        /// <summary>
        /// Get element value by name
        /// </summary>
        public HtmlElement GetControlByName(string name)
        {
            return (HtmlElement)GetType().GetField(name).GetValue(this);
        }

        /// <summary>
        /// Refresh all objects on the current page
        /// </summary>
        public void Refresh()
        {
            new PageBuilder().Build(this);
        }
    }

    public class Container : HtmlElement
    {
        public Container(SearchConfiguratiuon searchConfig)
            : base(searchConfig)
        {}

        public Container()
        {}
    }
}