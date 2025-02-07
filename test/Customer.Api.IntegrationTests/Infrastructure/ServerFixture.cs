using Customer.Api.Infrastructure.Data;
using Customer.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Customer.Api.IntegrationTests.Infrastructure;

public class ServerFixture : WebApplicationFactory<Program>
{
    private static Respawner? _respawner;
    public HttpClient Client { get; }

    public ServerFixture()
    {
        Client = CreateClient();
    }

    internal IServiceScope GetScope()
    {
        return Services.CreateScope();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");
        });
    }

    public async Task InitializeAsync()
    {
        _respawner ??= await CreateRespawner();
        await FromCleanState();
    }

    private async Task FromCleanState()
    {
        using var scope = GetScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        if (_respawner is null)
            throw new Exception("Respawner was not initialized yet");

        await _respawner.ResetAsync(configuration.GetConnectionString("Default")!);
    }

    public override async ValueTask DisposeAsync()
    {
        using var scope = Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<DbCustomer>();
        await dbContext.Database.EnsureDeletedAsync();
        await base.DisposeAsync();
    }

    private async Task<Respawner> CreateRespawner()
    {
        using var scope = Services.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        return await Respawner.CreateAsync(configuration.GetConnectionString("Default")!, new RespawnerOptions
        {
            TablesToIgnore = [],
            SchemasToExclude = [],
            WithReseed = true
        });
    }
}