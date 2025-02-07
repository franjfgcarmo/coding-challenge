using System.Net;
using Customer.Api.IntegrationTests.Infrastructure;
using Customer.Api.IntegrationTests.SeedWork;
using FluentAssertions;

namespace Customer.Api.IntegrationTests.Features.Customers;

[Collection(nameof(CollectionServerFixture))]
public class DeleteCustomerTest : FunctionalTestBase
{
    public DeleteCustomerTest(ServerFixture serverFixture) : base(serverFixture)
    {
    }

    [Fact]
    public async Task DeleteCustomer_ReturnsNoContent_WhenCustomerExists()
    {
        var customer = await ServerFixture.GivenADefaultCustomer();

        var response = await ServerFixture.Client.DeleteAsync(ApiDefinition.Customers.Delete(customer.Id));

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        DbCustomer.Customers.Any(a => a.Id == customer.Id).Should()
            .BeFalse();
    }

    [Fact]
    public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        var id = 999;
        var response = await ServerFixture.Client.DeleteAsync(ApiDefinition.Customers.Delete(id));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}