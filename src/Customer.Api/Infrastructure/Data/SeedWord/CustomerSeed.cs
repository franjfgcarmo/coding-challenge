using Customer.Domain.Entities;

namespace Customer.Api.Infrastructure.Data.SeedWord;

public class CustomerSeeds
{
    public static IEnumerable<Domain.Entities.Customer> DefaultCustomers()
    {
        return new List<Domain.Entities.Customer>
        {
            Domain.Entities.Customer.Create("Customer 1", "Customer 1", SexType.Female, "Address 1", "Country 1",
                "12345", "test@mail.com", DateTime.Now.AddYears(-20)),
            Domain.Entities.Customer.Create("Customer 2", "Customer 2", SexType.Female, "Address 2", "Country 2",
                "12345", "test@mail.com", DateTime.Now.AddYears(-20)),
            Domain.Entities.Customer.Create("Customer 3", "Customer 3", SexType.Female, "Address 3", "Country 3",
                "12345", "test@mail.com", DateTime.Now.AddYears(-20)),
            Domain.Entities.Customer.Create("Customer 4", "Customer 4", SexType.Female, "Address 4", "Country 4",
                "12345", "test@mail.com", DateTime.Now.AddYears(-20)),
            Domain.Entities.Customer.Create("Customer 5", "Customer 5", SexType.Female, "Address 5", "Country 5",
                "12345", "test@mail.com", DateTime.Now.AddYears(-20))
        };
    }
}