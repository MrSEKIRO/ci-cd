/*
 * // return all customers
app.MapGet("/customers", async (MyDbContext dbContext) =>
{
return await dbContext.Customers.ToListAsync();
}).WithName("GetCustomers").WithOpenApi();

// add a new customer
app.MapPost("/customers", async (MyDbContext dbContext, Customer customer) =>
{
dbContext.Customers.Add(customer);
await dbContext.SaveChangesAsync();
return Results.Created($"/customers/{customer.Id}", customer);
}).WithName("AddCustomer").WithOpenApi();
 * */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
	private readonly MyDbContext _dbContext;

	public CustomersController(MyDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	// GET: api/customers
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
	{
		return await _dbContext.Customers.ToListAsync();
	}

	// POST: api/customers
	[HttpPost]
	public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
	{
		_dbContext.Customers.Add(customer);
		await _dbContext.SaveChangesAsync();

		return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
	}

	// GET: api/customers/{id}
	[HttpGet("{id}")]
	public async Task<ActionResult<Customer>> GetCustomerById(int id)
	{
		var customer = await _dbContext.Customers.FindAsync(id);

		if(customer == null)
		{
			return NotFound();
		}

		return customer;
	}
}


