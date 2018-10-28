using System;
using System.Collections.Generic;
using System.Reflection;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;
using OpenQA.Selenium;

namespace AutomationFramework.WEB.Driver
{
    public class HtmlElementsCollectionBuilder : AbstractBuilder
    {
        /// <summary>
        /// Build the initialized instance of type by member info
        /// </summary>
        public object Build(MemberInfo memberInfo, SearchConfiguratiuon searchConfiguratiuon)
        {
            Type memberType = GetMemberType(memberInfo);
            var instance = Initialize(memberType, searchConfiguratiuon);
            return instance;
        }

        /// <summary>
        /// Return the list of webElements
        /// </summary>
        public List<TCollectionItem> BuildElementsCollectionList<TCollectionItem>(IList<IWebElement> nativeElementsList) where TCollectionItem : HtmlElement
        {
            List<TCollectionItem> buildedList = new List<TCollectionItem>();

            foreach (IWebElement webElement in nativeElementsList)
            {
                if (typeof(TCollectionItem).IsSubclassOf(typeof(Container)))
                {
                    var containerInstance = Activator.CreateInstance<TCollectionItem>();
                    containerInstance.NativeElement = webElement;
                    MemberInfo[] membersToBuild = GetElementsMemberInfo(containerInstance.GetType());

                    foreach (MemberInfo memberInfo in membersToBuild)
                    {
                        SearchConfiguratiuon searchConfiguratiuon = BuildSearchConfig(memberInfo, containerInstance);
                        Type memberType = GetMemberType(memberInfo);
                         var instance = Initialize(memberType, searchConfiguratiuon);
                        SetMemberValue(memberInfo, containerInstance, instance);
                    }
                    buildedList.Add(containerInstance);
                    continue;
                }

                if (typeof(TCollectionItem).IsSubclassOf(typeof(HtmlElement))|| typeof(TCollectionItem) == typeof(HtmlElement))
                {
                    var elementInstance = Activator.CreateInstance(typeof(TCollectionItem), webElement) as TCollectionItem;
                    buildedList.Add(elementInstance);
                }
            }
            return buildedList;
        }
    }
}
