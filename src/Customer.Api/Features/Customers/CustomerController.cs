namespace Customer.Api.Features.Customers;

[Route("api/customers")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Request all customers.", Description = "Request all customers.")]
    [ProducesResponseType(typeof(List<ListCustomer.CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new ListCustomer.CustomerQuery(), cancellationToken));
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Request a customer by ID.", Description = "Request a customer by ID.")]
    [ProducesResponseType(typeof(CustomerDetail.CustomerDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer(int id, CancellationToken cancellationToken = default)
    {
        var customer = await mediator.Send(new CustomerDetail.CustomerDetailQuery(id), cancellationToken);
        return customer is not null ? Ok(customer) : NotFound();
    }

    [HttpPost]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Create a new customer.", Description = "Create a new customer.")]
    [ProducesResponseType(typeof(CustomerCreate.CustomerCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreate.CustomerCreateCommand createCommand,
        CancellationToken cancellationToken = default)
    {
        var customer = await mediator.Send(createCommand, cancellationToken);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Update an existing customer.", Description = "Update an existing customer.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdate.CustomerUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        if (id != command.Id) return BadRequest();

        var result = await mediator.Send(command, cancellationToken);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Delete a customer by ID.", Description = "Delete a customer by ID.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new CustomerDelete.CustomerDeleteCommand(id), cancellationToken);
        return result ? NoContent() : NotFound();
    }
}