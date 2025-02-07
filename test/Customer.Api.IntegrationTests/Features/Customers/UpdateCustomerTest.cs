using System.Net;
using System.Net.Http.Json;
using Customer.Api.Features.Customers;
using Customer.Api.IntegrationTests.Infrastructure;
using Customer.Api.IntegrationTests.SeedWork;
using Customer.Domain.Entities;
using FluentAssertions;

namespace Customer.Api.IntegrationTests.Features.Customers;

public class UpdateCustomerTest : FunctionalTestBase
{
    public UpdateCustomerTest(ServerFixture serverFixture) : base(serverFixture)
    {
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsNoContent_WhenCustomerIsUpdated()
    {
        var customer = await ServerFixture.GivenADefaultCustomer();
        var updateCommand = new CustomerUpdate.CustomerUpdateCommand(customer.Id, "New name", "", SexType.Female,
            "Address 1", "Country 1",
            "12345", "tset@email.com", DateTime.Now.AddYears(-20));

        var response =
            await ServerFixture.Client.PutAsJsonAsync(ApiDefinition.Customers.Update(customer.Id), updateCommand);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedCustomer = await DbCustomer.Customers.FindAsync(customer.Id);
        updatedCustomer!.FirstName.Should().Be(updateCommand.FirstName);
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        var id = 999;
        var updateCommand = new CustomerUpdate.CustomerUpdateCommand(id, "New name", "", SexType.Female, "Address 1",
            "Country 1",
            "12345", "tset@email.com", DateTime.Now.AddYears(-20));

        var response =
            await ServerFixture.Client.PutAsJsonAsync(ApiDefinition.Customers.Update(id), updateCommand);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsBadRequest_WhenInvalidData()
    {
        var customer = await ServerFixture.GivenADefaultCustomer();
        var updateCommand = new CustomerUpdate.CustomerUpdateCommand(customer.Id, "", "", SexType.Female, "Address 1",
            "Country 1",
            "12345", "tset@email.com", DateTime.Now.AddYears(-20));

        var response =
            await ServerFixture.Client.PutAsJsonAsync(ApiDefinition.Customers.Update(customer.Id), updateCommand);

        response.StatusCode.Should()
            .Be(HttpStatusCode.BadRequest);
    }
}