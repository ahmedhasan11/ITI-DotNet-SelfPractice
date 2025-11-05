using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Iti
{
	internal class Program
	{
		static void Main(string[] args)
		{

			Context context=new Context();

			context.Departments.Add(new Department
			{ 
			Name="Dept 1"
			
			});

			context.SaveChanges();
		}
	}
}
