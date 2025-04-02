using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestProject.Controllers;

namespace TestProject.Tests
{
	public class DemoTest
	{
		[Fact]
		public void Test1()
		{
			Assert.True(true);
		}

		[Fact]
		public async Task CustomerIntegrationTest()
		{
			// Create DbContext
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables()
				.Build();

			var options = new DbContextOptionsBuilder<MyDbContext>()
				.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
				.Options;

			var dbContext = new MyDbContext(options);
			await dbContext.Database.EnsureCreatedAsync();

			// Just make sure delete all customers in db
			dbContext.Customers.RemoveRange(await dbContext.Customers.ToArrayAsync());

			// Create controller
			var controller = new CustomersController(dbContext);

			// Add customer
			var customer = new Customer { Name = "John" };
			var result = await controller.AddCustomer(customer);
			Assert.NotNull(result);

			// Check, does GetAll return the addes customer
			var customers = await controller.GetCustomers();
			Assert.Single(customers.Value);
			Assert.Equal("John", customers.Value.First().Name);
		}
	}
}