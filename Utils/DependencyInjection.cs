using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;
using drugstore_branch.Infrastructure.Repository;
using drugstore_branch.Infrastrucure.Repository;
using drugstore_branch.Infrastrucure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace drugstore_branch.Utils;

/*
 * The DependencyInjection class is used to register services and repositories 
 * in the dependency injection container. It defines a method to add 
 * infrastructure services for the application, including repositories and 
 * services for orders, products, and batches. The AutoMapper configuration 
 * is also added here to enable object mapping.
 */
public static class DependencyInjection
{
    /* 
     * This method registers all the necessary dependencies (services, 
     * repositories, and AutoMapper) into the DI container.
     * @param services - The IServiceCollection to register dependencies.
     * @return The IServiceCollection with the registered services.
     */
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IBatchRepository, BatchRepository>();

        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IBatchService, BatchService>();

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}