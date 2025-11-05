using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Iti
{
	internal class Context : DbContext
	{
		public DbSet<Department> Departments {get;set;}
		public DbSet<Employee> Employees { get; set; }
		public Context() :base(@"Data Source=DESKTOP-6N5TH8D\SQLEXPRESS;Initial Catalog=iti;Integrated Security=True;Pooling=False")
		{
		
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
