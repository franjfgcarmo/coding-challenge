using Customer.Domain;
using Customer.Domain.Entities;

namespace Customer.Api.Features.Customers;

public class CustomerDetail
{
    public record CustomerDetailQuery(int Id) : IRequest<CustomerDetailResponse>;

    public record CustomerDetailResponse(
        int Id,
        string FirstName,
        string LastName,
        SexType Sex,
        string Address,
        string Country,
        string PostalCode,
        string Email,
        DateTime Birthdate);

    public class CustomerDetailHandler : IRequestHandler<CustomerDetailQuery, CustomerDetailResponse?>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Customer, int> _repository;

        public CustomerDetailHandler(IRepository<Domain.Entities.Customer, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDetailResponse?> Handle(CustomerDetailQuery request,
            CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return customer is not null ? _mapper.Map<CustomerDetailResponse>(customer) : null;
        }

        public class CustomerProfile : Profile
        {
            public CustomerProfile()
            {
                CreateMap<Domain.Entities.Customer, CustomerDetailResponse>();
            }
        }
    }
}