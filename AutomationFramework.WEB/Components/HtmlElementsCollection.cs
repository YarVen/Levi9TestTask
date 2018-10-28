using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutomationFramework.WEB.Driver;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;
using DriverService = AutomationFramework.WEB.Driver.DriverService;

namespace AutomationFramework.WEB.Components
{
    public class HtmlElementsCollection<T> : IEnumerable<T> where T : HtmlElement
    {
        private IList<T> _items;

        /// <summary>
        /// Return the list of webElements
        /// </summary>
        public IList<T> Items
        {
            get
            {
                
                if (_items == null)
                {
                    IList<IWebElement> nativeItems;

                    if (SearchConfig.ParentContainer != null)
                        nativeItems = SearchConfig.ParentContainer.NativeElement.FindElements(
                            LocatorTransformer.GetNativeLocators(SearchConfig.FindBy));
                    else 
                        nativeItems = DriverService.Driver.FindElements(LocatorTransformer.GetNativeLocators(SearchConfig.FindBy));
                    _items = new HtmlElementsCollectionBuilder().BuildElementsCollectionList<T>(nativeItems);
                }
                return _items;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new HtmlElementsCollectionEnumerator<T>(Items);
        }

        public HtmlElementsCollection(SearchConfiguratiuon searchConfig)
        {
            SearchConfig = searchConfig;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get { return Items[index]; }
        }

        public SearchConfiguratiuon SearchConfig
        {
            get; set; 
        }

        public void Refresh()
        {
            Thread.Sleep(200);
            _items = null;
        }
    }

    public class HtmlElementsCollectionEnumerator<T> : IEnumerator<T> where T:HtmlElement
    {
        private T[] _collection;
        private int curIndex;
        private T currentItem;


        public HtmlElementsCollectionEnumerator(IEnumerable<T> elements)
        {
            _collection = elements.ToArray();
            curIndex = -1;
            currentItem = default(T);

        }

        public bool MoveNext()
        {
            if (++curIndex >= _collection.Length)
            {
                return false;
            }
            else
            {
                currentItem = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        void IDisposable.Dispose() { }

        public T Current
        {
            get { return currentItem; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

    }
}
