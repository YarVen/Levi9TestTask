using System;
using System.Reflection;
using AutomationFramework.WEB.Components;
using AutomationFramework.WEB.Search;

namespace AutomationFramework.WEB.Driver
{
    internal class ContainerBuilder:AbstractBuilder
    {
        private HtmlElement _currentContainer;

        /// <summary>
        /// Return the webElement container instance
        /// </summary>
        public object Build(MemberInfo containerMemberInfo,SearchConfiguratiuon configuration)
        {
            Type containerType = GetMemberType(containerMemberInfo);
            var containerInstance = Initialize(containerType, configuration);
            _currentContainer = (HtmlElement)containerInstance;
            MemberInfo[] membersToBuild = GetElementsMemberInfo(containerType);
         
            foreach (MemberInfo memberInfo in membersToBuild)
            {
                SearchConfiguratiuon searchConfiguratiuon = BuildSearchConfig(memberInfo, _currentContainer);
                Type memberType = GetMemberType(memberInfo);

                if (memberType.IsGenericType &&
                    memberType.GetGenericTypeDefinition() == typeof (HtmlElementsCollection<>))
                {
                    object instance = new HtmlElementsCollectionBuilder().Build(memberInfo, searchConfiguratiuon);
                    SetMemberValue(memberInfo, containerInstance, instance);
                    continue;
                }
                if (memberType.IsSubclassOf(typeof(Container)))
                {
                    object instance = new ContainerBuilder().Build(memberInfo, searchConfiguratiuon);
                    SetMemberValue(memberInfo, containerInstance, instance);
                    continue;
                }
                if (memberType.IsSubclassOf(typeof (HtmlElement))||memberType==typeof(HtmlElement))
                {
                    object instance = base.Initialize(memberType, searchConfiguratiuon);
                    SetMemberValue(memberInfo, containerInstance, instance);
                }
            }
            return containerInstance;
        }

        /// <summary>
        /// Initialize the container instance
        /// </summary>
        protected override object Initialize(Type type, SearchConfiguratiuon configuration)
        {
            var instance = Activator.CreateInstance(type);
            ((Container) instance).SearchConfig = configuration;
            return instance;
        }
    }
}
