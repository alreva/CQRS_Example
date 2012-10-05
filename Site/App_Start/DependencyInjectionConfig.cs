using CQRS;
using Model;
using ReadModel;
using ReadModel.Infrastructure;

namespace Site.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureDependencies(IDependencyInjectionRegistrationContainer dependencyInjectionRegistrationContainer)
        {
            dependencyInjectionRegistrationContainer
                .RegisterType<IGetCatalogTreeQueryService, GetCatalogTreeQueryService>()
                .RegisterType<IGetAvailableCategoriesQueryService, GetAvailableCategoriesQueryService>()
                .RegisterType<IGetAvailableCategoryByIdQueryService, GetAvailableCategoryByIdQueryService>()

                .RegisterType<ICommandSender, CommandSender>()
                .RegisterNamedType<IHandle, AddCategoryHandler>("add-category")
                .RegisterType<IRepository<Category>, Repository<Category>>()
                .RegisterType<IEventStore, AdoNetEventStore>()
                .RegisterType<IToEventConverter, ToEventConverter>()

                .RegisterType<IEventPublisher, EventPublisher>()

                .RegisterNamedType<IEventHandler, UpdateCategoryTreeOnCategoryAdded>("UpdateCategoryTreeOnCategoryAdded")
                .RegisterNamedType<IEventHandler, UpdateAvailableCategoriesOnCategoryAdded>("UpdateAvailableCategoriesOnCategoryAdded")

                .RegisterType<IAdoNetConnectionProvider, AdoNetConnectionProvider>("DefaultConnection")
                .RegisterType<IReadModelRemover, ReadModelRemover>()
                .RegisterType<IEventPlayer, EventPlayer>();
        }
    }
}