using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ProjectLothal.Elastic.Core.Consumers;
using System.Reflection;

namespace ProjectLothal.Elastic.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSubClassOfType(Assembly.GetExecutingAssembly(), typeof(Consumers));
            return services;
        }

        public static IServiceCollection AddSubClassOfType(this IServiceCollection services, Assembly assembly, Type type, Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
        {

            var types = assembly.GetTypes().Where(mytype => mytype.GetInterfaces().Contains(type)).ToList();
            foreach (var item in types)
            {
                if (addWithLifeCycle == null)

                    services.AddMediator(x => x.AddConsumersFromNamespaceContaining(item));
                else addWithLifeCycle(services, type);

            }
            return services;
        }
    }
}
