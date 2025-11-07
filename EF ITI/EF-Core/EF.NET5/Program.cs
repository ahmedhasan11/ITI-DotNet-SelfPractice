using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace EF.NET5
{
	internal class Program
	{
		static void Main(string[] args)
		{

			CoreContext context = new CoreContext();
			#region Eager Loading: Include 
			//Eager Loading
			//Inculde hna ay 7aga ht3mlha include f enta bt3ml include mn el Departments
			//lw 3ayz a3ml include mn a5r 7aga 3amlha include msln 3la employees
			//sa3tha hnst5dm ThenInclude


			//var query = context.Departments.AsSplitQuery()
			//	.Include(d => d.Projects)
			//	.Include(d => d.Employees)
			//	.ThenInclude(e => e.Attendances);


			//foreach (var dept in query)
			//{
			//	foreach (var emp in dept.Employees)
			//	{
			//		Console.WriteLine(emp.Name);

			//	}
			//	Console.WriteLine(dept.Name);
			//}
			#endregion

			#region Explicit Loading : Query
			//mn esmo kda ana htlbmno eno y load el collection da dlw2ty
			//var query = context.Departments.ToList();

			//foreach (var dept in query)
			//{
			//	context.Entry(dept).Collection(d => d.Employees).Load();//Explicit Loading of collection of employees
			//	context.Entry(dept).Reference(d => d.Branch).Load();//Explicit Loading of object of Branch
			//	var emps = context.Entry(dept).Collection(d => d.Employees).Query().Where(e => e.ID < 10);
			//	foreach (var emp in dept.Employees)
			//	{
			//		Console.WriteLine(emp.Name);

			//	}
			//	Console.WriteLine(dept.Name);
			//}

			#endregion

			#region Lazy Loading
			//1-install Package efcore.Proxies

			#endregion

			#region AsNoTracking
			//	var query = context.Employees.AsNoTracking()
			//.Include(e => e.Department);

			//	var query2 = context.Employees.AsNoTrackingWithIdentityResolution()
			//	.Include(e => e.Department); 
			#endregion

			//foreach (var dept in query)
			//{
			//	foreach (var emp in dept.Employees)
			//	{
			//		Console.WriteLine(emp.Name);

			//	}
			//             Console.WriteLine(dept.Name);
			//         }

			var dept = context.Departments.First();

			//how to access the shadow property
			context.Entry(dept).Property("Deleted").CurrentValue=true; //b2olo 5ly el department da deleted
			context.SaveChanges();
			//tb lw 3ayzo gwa query??

			var query=  //bgeb kol el nas elle deleted bta3thom = true
				from d in context.Departments
				where EF.Property<bool>(d, "deleted") ==true //class EF da m3mol f el video (mktbto4)
				select d;

		}
	}
}