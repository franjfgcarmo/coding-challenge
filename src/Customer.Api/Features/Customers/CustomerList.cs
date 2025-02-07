using Customer.Domain;
using Customer.Domain.Entities;

namespace Customer.Api.Features.Customers;

public class CustomerList
{
    public record CustomerResponse(
        int Id,
        string FirstName,
        string LastName,
        SexType Sex,
        string Address,
        string Country,
        string PostalCode,
        string Email,
        DateTime Birthdate);

    public record CustomerQuery : IRequest<IEnumerable<CustomerResponse>>;

    public class CustomerListHandler : IRequestHandler<CustomerQuery, IEnumerable<CustomerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Customer, int> _repository;

        public CustomerListHandler(IRepository<Domain.Entities.Customer, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(CustomerQuery request,
            CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
        }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Entities.Customer, CustomerResponse>();
        }
    }
}