using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Site
{
    public static class DependencyInjection
    {
        private static IDependencyInjectionRegistrationContainer _registrationContainer;
        private static IUnityContainer _unityContainer;

        public static IDependencyInjectionRegistrationContainer RegistrationContainer
        {
            get { return _registrationContainer ?? (_registrationContainer = new UnityBasedDependencyInjectionRegistrationContainer(DependencyResolver.Current, UnityContainerInstance)); }
        }

        private static IUnityContainer UnityContainerInstance
        {
            get { return _unityContainer ?? (_unityContainer = new UnityContainer()); }
        }
    }
}