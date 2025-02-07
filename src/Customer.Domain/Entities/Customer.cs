using Customer.Domain.Exceptions;

namespace Customer.Domain.Entities;

public class Customer : BaseEntity<int>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public SexType Sex { get; private set; }
    public string Address { get; private set; } = null!;
    public string Country { get; private set; } = null!;

    public DateTime Birthdate { get; private set; }
    public string PostalCode { get; private set; } = null!;
    public string Email { get; private set; } = null!;


    public static Customer Create(string firstName, string lastName, SexType sex, string address, string country,
        string postalCode, string email, DateTime birthdate)
    {
        GuardAgainstCustomerInvalid(firstName, birthdate);

        return new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Sex = sex,
            Address = address,
            Country = country,
            PostalCode = postalCode,
            Email = email,
        };
    }


    public void Update(string firstName, string lastName, SexType sex, string address, string country,
        string postalCode, string email, DateTime birthdate)
    {
        GuardAgainstCustomerInvalid(firstName, birthdate);

        FirstName = firstName;
        LastName = lastName;
        Sex = sex;
        Address = address;
        Country = country;
        PostalCode = postalCode;
        Email = email;
    }

    private Customer()
    {
    }

    private static void GuardAgainstCustomerInvalid(string firstName, DateTime birthdate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw CustomerException.NameIsRequired();
        }

        var age = DateTime.Today.Year - birthdate.Year;
        if (birthdate.Date > DateTime.Today.AddYears(-age)) age--;

        if (age < 18)
        {
            throw CustomerException.MustBeAtLeast18YearsOld();
        }
    }
}