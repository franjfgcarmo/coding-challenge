namespace Customer.Api.IntegrationTests.Infrastructure;

[CollectionDefinition(nameof(CollectionServerFixture))]
public class CollectionServerFixture : ICollectionFixture<ServerFixture>
{
}