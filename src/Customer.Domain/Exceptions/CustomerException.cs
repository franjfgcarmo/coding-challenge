namespace Customer.Domain.Exceptions;

[Serializable]
public class CustomerException : DomainException

{
    private CustomerException(string message) : base(message)
    {
    }

    public static CustomerException NameIsRequired()
    {
        return new CustomerException("Name is required");
    }

    public static Exception MustBeAtLeast18YearsOld()
    {
        return new CustomerException("Customer must be at least 18 years old");
    }
}