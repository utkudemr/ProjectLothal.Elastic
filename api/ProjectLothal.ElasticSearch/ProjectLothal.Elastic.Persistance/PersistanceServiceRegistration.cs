

using Microsoft.Extensions.DependencyInjection;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Persistance.Repositories;

namespace ProjectLothal.Elastic.Persistance;


public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}

