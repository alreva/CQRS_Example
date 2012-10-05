using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Site.App_Start;
using Site.Controllers;
using Assert = NUnit.Framework.Assert;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Site.Unit.Tests
{
    [TestClass]
    public class ControllerDependenciesIntegrityTests2
    {
        [TestMethod]
        public void AllControllerDependenciesResolvedTests()
        {
            DependencyInjectionConfig.ConfigureDependencies(DependencyInjection.RegistrationContainer);

            var allControllerTypes =
                typeof(HomeController)
                .Assembly
                .GetTypes()
                .Where(controllerTypeCandidate => typeof(Controller).IsAssignableFrom(controllerTypeCandidate)).ToArray();

            foreach (var controllerType in allControllerTypes)
            {
                var controllerInstance = DependencyInjection.RegistrationContainer.DependencyResolver.GetService(controllerType);
                MSAssert.IsInstanceOfType(controllerInstance, typeof(Controller));
            }
        }
    }
}
