using Customer.Api.Infrastructure.Data.Config;
using Customer.Domain;
using Customer.Domain.Entities;

namespace Customer.Api.Features.Customers;

public class CustomerUpdate
{
    public record CustomerUpdateCommand(
        int Id,
        string FirstName,
        string LastName,
        SexType Sex,
        string Address,
        string Country,
        string PostalCode,
        string Email,
        DateTime Birthdate) : IRequest<bool>;


    public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Customer, int> _repository;

        public CustomerUpdateHandler(IRepository<Domain.Entities.Customer, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null) return false;

            customer.Update(request.FirstName, request.LastName, request.Sex, request.Address, request.Country,
                request.PostalCode, request.Email, request.Birthdate);
            await _repository.CommitChanges(cancellationToken);
            return true;
        }
    }

    public class CustomerCreateCommandValidator : AbstractValidator<CustomerUpdateCommand>
    {
        public CustomerCreateCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(DatabaseConstants.Length100)
                .WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.LastName)
                .MaximumLength(DatabaseConstants.Length100)
                .WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email is invalid.")
                .MaximumLength(DatabaseConstants.Length100)
                .WithMessage("Email must not exceed 100 characters.");
            RuleFor(x => x.Sex)
                .IsInEnum()
                .WithMessage("Sex is required.");
            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.");
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code is required.")
                .MaximumLength(DatabaseConstants.Length5)
                .WithMessage("Postal code must be 5 characters.");
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(DatabaseConstants.Length250)
                .WithMessage("Address must not exceed 250 characters.");
        }
    }

    public class CustomerUpdateCommandValidator : AbstractValidator<CustomerUpdateCommand>
    {
        public CustomerUpdateCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name is required.");
        }
    }
}