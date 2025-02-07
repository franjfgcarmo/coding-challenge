using Customer.Api.IntegrationTests.Infrastructure;
using Customer.Domain.Entities;

namespace Customer.Api.IntegrationTests.SeedWork;

public static class GivenCustomerExtensions
{
    public static async Task<Domain.Entities.Customer> GivenADefaultCustomer(
        this ServerFixture serverFixture)
    {
        var customer = Domain.Entities.Customer.Create("New name", "last name", SexType.Female, "Address 1",
            "Country 1",
            "00001", "tset@email.com", DateTime.Now.AddYears(-20));

        await serverFixture.GivenEntityAsync(customer);

        return customer;
    }

    public static async Task<IEnumerable<Domain.Entities.Customer>> GivenSomeDefaultCustomers(
        this ServerFixture serverFixture, int count = 3)
    {
        var customers = new List<Domain.Entities.Customer>();

        for (int i = 0; i < count; i++)
        {
            customers.Add(Domain.Entities.Customer.Create($"New name {i}", $"last name {i}", SexType.Female,
                $"Address {i}", $"Country {i}",
                $"0000{i}", "tset@email.com", DateTime.Now.AddYears(-20)));
        }

        await serverFixture.GivenEntityEnumerableAsync(customers);

        return customers;
    }
}