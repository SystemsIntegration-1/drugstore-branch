using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;
using drugstore_branch.Infrastructure.Repository;
using drugstore_branch.Infrastrucure.Repository;
using drugstore_branch.Infrastrucure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace drugstore_branch.Utils;

public static class DependencyInjection
{
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