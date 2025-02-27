﻿using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;
using drugstore_branch.Infrastrucure.Repository;
using drugstore_branch.Infrastrucure.Service;

namespace drugstore_branch.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}