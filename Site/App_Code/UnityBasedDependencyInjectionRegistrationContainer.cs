using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Site
{
    public class UnityBasedDependencyInjectionRegistrationContainer : IDependencyInjectionRegistrationContainer
    {
        private readonly IDependencyResolver _baseResolver;
        private readonly IUnityContainer _unityContainer;
        private IDependencyResolver _dependencyResolver;

        public UnityBasedDependencyInjectionRegistrationContainer(
            IDependencyResolver baseResolver,
            IUnityContainer unityContainer)
        {
            _baseResolver = baseResolver;
            _unityContainer = unityContainer;
        }

        public IDependencyInjectionRegistrationContainer RegisterType<TAbstraction, TImplementation>(params object[] constructorParameters) where TImplementation : TAbstraction
        {
            if (constructorParameters == null || !constructorParameters.Any())
            {
                _unityContainer.RegisterType<TAbstraction, TImplementation>();
            }
            else
            {
                _unityContainer.RegisterType<TAbstraction, TImplementation>(new InjectionConstructor(constructorParameters));
            }
            return this;
        }

        public IDependencyInjectionRegistrationContainer RegisterNamedType<TAbstraction, TImplementation>(string name, params object[] constructorParameters) where TImplementation : TAbstraction
        {
            if (constructorParameters == null || !constructorParameters.Any())
            {
                _unityContainer.RegisterType<TAbstraction, TImplementation>(name);
            }
            else
            {
                _unityContainer.RegisterType<TAbstraction, TImplementation>(name, new InjectionConstructor(constructorParameters));
            }
            return this;
        }

        public IDependencyResolver DependencyResolver
        {
            get { return _dependencyResolver ?? (_dependencyResolver = new UnityBasedDependencyResolver(_baseResolver, _unityContainer)); }
        }
    }
}