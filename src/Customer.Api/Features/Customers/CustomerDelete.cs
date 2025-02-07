using Customer.Domain;

namespace Customer.Api.Features.Customers;

public class CustomerDelete
{
    public record CustomerDeleteCommand(int Id) : IRequest<bool>;

    public class CustomerDeleteHandler : IRequestHandler<CustomerDeleteCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Customer, int> _repository;

        public CustomerDeleteHandler(IRepository<Domain.Entities.Customer, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null) return false;

            await _repository.DeleteAsync(customer, cancellationToken);
            await _repository.CommitChanges(cancellationToken);
            return true;
        }
    }
}