using Customer.Api.Infrastructure.Data;
using Customer.Api.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Api.IntegrationTests.SeedWork;

public static class ServerFixtureExtensions
{
    public static async Task<ServerFixture> GivenEntityAsync<T>(this ServerFixture serverFixture, T entity)
        where T : class
    {
        using var scope = serverFixture.GetScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<DbCustomer>();
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return serverFixture;
    }

    public static async Task<ServerFixture> GivenEntityEnumerableAsync<T>(this ServerFixture serverFixture,
        IEnumerable<T> entities) where T : class
    {
        using var scope = serverFixture.GetScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<DbCustomer>();
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        return serverFixture;
    }
}