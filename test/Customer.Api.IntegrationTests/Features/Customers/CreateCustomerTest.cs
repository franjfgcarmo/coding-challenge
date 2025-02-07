using System.Net;
using Customer.Api.Features.Customers;
using Customer.Api.IntegrationTests.Infrastructure;
using Customer.Domain.Entities;
using FluentAssertions;

namespace Customer.Api.IntegrationTests.Features.Customers;

public class CreateCustomerTest : FunctionalTestBase
{
    public CreateCustomerTest(ServerFixture serverFixture) : base(serverFixture)
    {
    }

    [Fact]
    public async Task CreateCustomer_ReturnsCreated_WhenCustomerIsCreated()
    {
        var createCustomer = new CustomerCreate.CustomerCreateCommand("New name", "", SexType.Female, "Address 1",
            "Country 1",
            "12345", "tset@email.com", DateTime.Now.AddYears(-20));

        var response = await ServerFixture.Client.PostWithContentAsync(ApiDefinition.Customers.Create(), createCustomer);

        response.StatusCode.Should()
            .Be(HttpStatusCode.Created);
        
        // var customerCreated = await DbCustomer.Customers.FindAsync(createCustomer.Id);
        // customerCreated!.FirstName.Should().Be(customerCreated.FirstName);
    }

    [Fact]
    public async Task CreateCustomer_ReturnsBadRequest_WhenInvalidData()
    {
        var createCommand = new CustomerCreate.CustomerCreateCommand("", "", SexType.Female, "Address 1", "Country 1",
            "12345", "tset@email.com", DateTime.Now.AddYears(-20));

        var response = await ServerFixture.Client.PostWithContentAsync(ApiDefinition.Customers.Create(), createCommand);

        response.StatusCode.Should()
            .Be(HttpStatusCode.BadRequest);
    }
}