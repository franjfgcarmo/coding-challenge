using System.Net;
using Customer.Api.Features.Customers;
using Customer.Api.IntegrationTests.Infrastructure;
using Customer.Api.IntegrationTests.SeedWork;
using FluentAssertions;

namespace Customer.Api.IntegrationTests.Features.Customers;

[Collection(nameof(CollectionServerFixture))]
public class GetCustomerTest : FunctionalTestBase
{
    public GetCustomerTest(ServerFixture serverFixture) : base(serverFixture)
    {
    }

    [Fact]
    public async Task GetCustomer_ReturnsOk_WhenCustomerExists()
    {
        var customer = await ServerFixture.GivenADefaultCustomer();

        var response = await ServerFixture.Client.GetAsync(ApiDefinition.Customers.Get(customer.Id));

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);

        var customerDetailResponse = (await response.Content.ReadAsAsync<CustomerDetail.CustomerDetailResponse>())!;
        customerDetailResponse.Should()
            .NotBeNull();
        customerDetailResponse.Should()
            .BeEquivalentTo(customer);
    }

    [Fact]
    public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        var id = 999;
        var response = await ServerFixture.Client.GetAsync(ApiDefinition.Customers.Get(id));

        response.StatusCode.Should()
            .Be(HttpStatusCode.NotFound);
    }
}