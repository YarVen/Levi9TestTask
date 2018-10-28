using System;
using Ninject;
using Ninject.Modules;

namespace AutomationFramework.WEB.Driver
{
    internal class DependencyManager
    {
        private static Dependencies _ninjectModule;
        private static IKernel _kernel;

        private static Dependencies _ninject
        {
            get { return _ninjectModule ?? (_ninjectModule = new Dependencies()); }
        }

        public static IKernel Kernel
        {
            get { return _kernel ?? (_kernel = new StandardKernel(_ninject)); }
        }

        /// <summary>
        /// Add webDriver dependency 
        /// </summary>
        /// <param name="interfaceQualified"></param>
        /// <param name="implementationQalified"></param>
        public static void AddDependency(string interfaceQualified, string implementationQalified)
        {
            var interfaceType = Type.GetType(interfaceQualified);
            
            if (interfaceType==null)
                throw new ClassOrInterfaceNotFound(interfaceQualified);


            var implementationType = Type.GetType(implementationQalified);

            if (implementationType == null)
                throw new ClassOrInterfaceNotFound(implementationQalified);

            Kernel.Rebind(interfaceType).To(implementationType);
        }
    }

    public class Dependencies : NinjectModule
    {
        /// <summary>
        /// Reserve mapping
        /// </summary>
        public override void Load()
        {
            Bind<ISeleniumDriverBuilder>().To<DefaultSeleniumDriverBuilder>();
        }
    }
}
