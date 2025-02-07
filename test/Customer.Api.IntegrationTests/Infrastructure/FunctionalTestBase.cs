using Customer.Api.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Api.IntegrationTests.Infrastructure;

[Collection(nameof(CollectionServerFixture))]
public class FunctionalTestBase : IAsyncLifetime
{
    protected readonly ServerFixture ServerFixture;
    protected DbCustomer DbCustomer { get; private set; }

    protected FunctionalTestBase(ServerFixture serverFixture)
    {
        ServerFixture = serverFixture;
        DbCustomer = serverFixture.GetScope().ServiceProvider.GetRequiredService<DbCustomer>();
    }

    public async Task InitializeAsync() => await ServerFixture.InitializeAsync();

    public Task DisposeAsync() => Task.CompletedTask;
}