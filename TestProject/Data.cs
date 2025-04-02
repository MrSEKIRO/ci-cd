using Microsoft.EntityFrameworkCore;

namespace TestProject
{
	public class Customer
	{
        public int Id { get; set; }
		public string Name { get; set; }
	}

	public class MyDbContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }

		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}
	}
}
