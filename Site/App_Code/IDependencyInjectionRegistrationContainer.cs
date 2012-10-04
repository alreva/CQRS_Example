using System.Web.Mvc;

namespace Site
{
    public interface IDependencyInjectionRegistrationContainer
    {
        IDependencyInjectionRegistrationContainer RegisterType<TAbstraction, TImplementation>(params object[] constructorParameters) where TImplementation : TAbstraction;

        IDependencyInjectionRegistrationContainer RegisterNamedType<TAbstraction, TImplementation>(string name, params object[] constructorParameters) where TImplementation : TAbstraction;

        IDependencyResolver DependencyResolver { get; }
    }
}