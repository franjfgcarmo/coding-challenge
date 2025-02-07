using Customer.Domain.Entities;
using Customer.Domain.Exceptions;

namespace Customer.Domain.UnitTest;

public class CustomerTest
{
    [Fact]
    public void Create_Customer()
    {
        var name = "John Doe";

        var customer = Entities.Customer.Create(name, "", SexType.Female, "Address 1", "Country 1",
            "Postal Code 1", "tset@email.com", DateTime.Now.AddYears(-20));

        customer.FirstName.Should().Be(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WithWhitespace_Or_Null_Name_ShouldThrowException(string name)
    {
        Action act = () => Entities.Customer.Create(name, "New name", SexType.Female, "Address 1", "Country 1",
            "Postal Code 1", "tset@email.com", DateTime.Now.AddYears(-20));

        act.Should()
            .Throw<CustomerException>()
            .Where(w => w.Message.Contains("Name is required"));
    }
    
   [Fact]
    public void If_Try_Create_A_Customer_Under_18_Years_Old_ShouldThrowException()
    {
        Action act = () => Entities.Customer.Create("new customer name", "New name", SexType.Female, "Address 1", "Country 1",
            "Postal Code 1", "tset@email.com", DateTime.Now);

        act.Should()
            .Throw<CustomerException>()
            .Where(w => w.Message.Contains("Customer must be at least 18 years old"));
    }
}