using Customer.Domain;
using Customer.Domain.Entities;

namespace Customer.Api.Features.Customers;

public class CustomerCreate
{
    public record CustomerCreateCommand(
        string FirstName,
        string LastName,
        SexType Sex,
        string Address,
        string Country,
        string PostalCode,
        string Email,
        DateTime Birthdate) : IRequest<CustomerCreateResponse>;

    public record CustomerCreateResponse(
        int Id,
        string FirstName,
        string LastName,
        SexType Sex,
        string Address,
        string Country,
        string PostalCode,
        string Email,
        DateTime Birthdate);

    public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, CustomerCreateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Customer, int> _repository;

        public CustomerCreateHandler(IRepository<Domain.Entities.Customer, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerCreateResponse> Handle(CustomerCreateCommand request,
            CancellationToken cancellationToken)
        {
            var customer = Domain.Entities.Customer.Create(request.FirstName, request.LastName, request.Sex,
                request.Address, request.Country, request.PostalCode, request.Email, request.Birthdate);
            await _repository.CreateAsync(customer, cancellationToken);
            await _repository.CommitChanges(cancellationToken);
            return _mapper.Map<CustomerCreateResponse>(customer);
        }

        public class CustomerCreateCommandValidator : AbstractValidator<CustomerCreateCommand>
        {
            public CustomerCreateCommandValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("Name is required.")
                    .MaximumLength(100)
                    .WithMessage("Name must not exceed 100 characters.");
                RuleFor(x => x.LastName)
                    .MaximumLength(100)
                    .WithMessage("Name must not exceed 100 characters.");
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email is required.")
                    .EmailAddress()
                    .WithMessage("Email is invalid.")
                    .MaximumLength(100)
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
                    .MaximumLength(5)
                    .WithMessage("Postal code must be 5 characters.");
                RuleFor(x => x.Address)
                    .NotEmpty()
                    .WithMessage("Address is required.")
                    .MaximumLength(250)
                    .WithMessage("Address must not exceed 250 characters.");
            }
        }

        public class CustomerProfile : Profile
        {
            public CustomerProfile()
            {
                CreateMap<Domain.Entities.Customer, CustomerCreateResponse>();
            }
        }
    }
}