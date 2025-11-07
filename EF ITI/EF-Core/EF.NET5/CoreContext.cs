using EF.NET5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.NET5
{
    internal class CoreContext:DbContext
	{
		public CoreContext() 
		{
			//ay query gy mn el database mt3ml4 track 3leh
			//ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}


		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Branch> Branches { get; set; }

		//3shan n7dd elprovider bta3na elle hnst5dmo(sql server)
		//3shan kda hnst5dm el onconfigure method
		//hy7sl l el method call w enta bt configure el models aw EF layer


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data source=.\sqlexpress;Initial catalog= itiCore;integrated security=true");
			base.OnConfiguring(optionsBuilder);


			//optionsBuilder.UseLazyLoadingProxies(true);  //Enable LazyLoading 
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Change Table name
			//modelBuilder.Entity<Department>().ToTable("Department");
			//make Required
			//modelBuilder.Entity<Department>().Property(d => d.Name).IsRequired(true);
			//make Composite Key
			modelBuilder.Entity<Attendance>().HasKey(a => new {a.EmployeeID , a.Date });

			//how to make a shadow property ,, m4 btzhr fel model bs btt7t fel DB
			//you can access it by 2 ways
			//1-query filter
			//2-access in application
			modelBuilder.Entity<Department>()
				.Property<bool>("Deleted").IsRequired(true).HasDefaultValue(false);

			//GetEntityTypes() btgeblk kol el Datasets aw el Entities
			foreach (var item in modelBuilder.Model.GetEntityTypes())
			{
				modelBuilder.Entity(item.Name) //item.name hya esm el entity f kol cycle
				.Property<bool>("Deleted").IsRequired(true).HasDefaultValue(false);
				//kda 7tet el property de f kol el entities
			}

			base.OnModelCreating(modelBuilder);
		}

	}
}
