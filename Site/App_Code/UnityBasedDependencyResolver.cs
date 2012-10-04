using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Linq;
using ReadModel;

namespace Site
{
    public class UnityBasedDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver _baseResolver;
        private readonly IUnityContainer _unityContainer;

        public UnityBasedDependencyResolver(
            IDependencyResolver baseResolver,
            IUnityContainer unityContainer)
        {
            _baseResolver = baseResolver;
            _unityContainer = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _unityContainer.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                var instance = _baseResolver.GetService(serviceType);

                /*
                _unityContainer.Resolve<IGetAvailableCategoriesQueryService>();
                */

                if (instance == null)
                {
                    return null;
                }

                _unityContainer.RegisterInstance(serviceType, instance);
                return instance;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                var instances = _baseResolver.GetServices(serviceType).ToArray();
                instances.ToList().ForEach(instance => _unityContainer.RegisterInstance(serviceType, instance));
                return instances;
            }
        }
    }
}