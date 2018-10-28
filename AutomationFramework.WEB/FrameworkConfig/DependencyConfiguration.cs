using System.Configuration;

namespace AutomationFramework.WEB.FrameworkConfig
{
    public class Dependency : ConfigurationElement
    {
        [ConfigurationProperty("interface")]
        public string Interface
        {
            get { return this["interface"].ToString(); }
        }

        [ConfigurationProperty("implementation")]
        public string Implementation
        {
            get { return this["implementation"].ToString(); }
        }
    }


    public class DependencySectionHandler : ConfigurationSection
    {
        /// <summary>
        /// Get the dependency configuration config
        /// </summary>
        public static DependencySectionHandler GetConfig()
        {
            return (DependencySectionHandler)ConfigurationManager.GetSection("dependencyConfiguration") ??
                   new DependencySectionHandler();
        }

        [ConfigurationProperty("dependencies")]
        [ConfigurationCollection(typeof(Dependencies), AddItemName = "dependency")]
        public Dependencies Dependencies
        {
            get
            {
                return this["dependencies"] as Dependencies;
            }
        }
    }

    public class Dependencies: ConfigurationElementCollection
    {
        public Dependency this[int index]
        {
            get
            {
                return BaseGet(index) as Dependency ;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Dependency();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Dependency) element);
        }
    }
}
