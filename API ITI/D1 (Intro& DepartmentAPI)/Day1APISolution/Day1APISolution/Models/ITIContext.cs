using Microsoft.EntityFrameworkCore;

namespace Day1APISolution.Models
{
	public class ITIContext:DbContext
	{
		public DbSet<Department> Departments { get; set; }

		public ITIContext(DbContextOptions<ITIContext> options):base(options)
		{

		}
	}
}
