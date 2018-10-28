using System;

namespace AutomationFramework.WEB.Driver
{
    public class ClassOrInterfaceNotFound:ApplicationException
    {
        public ClassOrInterfaceNotFound(string entityname)
            : base((string.Format("Unable to locate class or interface: {0}. Perhaps you forgot to add assemvly name after a comma?", entityname)))
        {}
    }
}
