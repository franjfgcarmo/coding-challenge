using Customer.Api.Features.Customers;
using Customer.Api.Infrastructure.Behaviours;
using Customer.Api.Infrastructure.Data;
using Customer.Domain;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped(typeof(IRepository<,>), typeof(DbRepository<,>));
        services.AddFluentValidationAutoValidation(config => { config.DisableDataAnnotationsValidation = true; });
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CustomerCreate.CustomerCreateHandler.CustomerCreateCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddDbContext<DbCustomer>(
            o => o.UseSqlServer(
                configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("Customer.Host")));

        return services;
    }
}