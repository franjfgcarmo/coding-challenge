using System.Net;
using Customer.Api.Features.Customers;
using Customer.Api.IntegrationTests.Infrastructure;
using Customer.Api.IntegrationTests.SeedWork;
using FluentAssertions;

namespace Customer.Api.IntegrationTests.Features.Customers;

public class ListCustomerTest : FunctionalTestBase
{
    public ListCustomerTest(ServerFixture serverFixture) : base(serverFixture)
    {
    }

    [Fact]
    public async Task Response_Ok_There_Are_Not_Data()
    {
        var response = await ServerFixture.Client.GetAsync(ApiDefinition.Customers.All());

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseActivityDispatcher =
            (await response.Content.ReadAsAsync<IEnumerable<ListCustomer.CustomerResponse>>())!;

        responseActivityDispatcher.Should().NotBeNull();
        responseActivityDispatcher.Should().HaveCount(0);
    }

    [Fact]
    public async Task Response_Ok_There_Are_Data()
    {
        var count = 5;
        await ServerFixture.GivenSomeDefaultCustomers(count);

        var response = await ServerFixture.Client.GetAsync(ApiDefinition.Customers.All());

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseActivityDispatcher =
            (await response.Content.ReadAsAsync<IEnumerable<ListCustomer.CustomerResponse>>())!;

        responseActivityDispatcher.Should()
            .NotBeNull();
        responseActivityDispatcher.Should()
            .HaveCount(count);
    }
}