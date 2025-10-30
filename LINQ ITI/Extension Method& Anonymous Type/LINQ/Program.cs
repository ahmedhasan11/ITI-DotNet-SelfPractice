using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Extenstion Method
			int X = 12345;
			int Y = Int32Extensions.Mirror(X);
			Y = X.Mirror(); //Extension Methd Call
			//Console.WriteLine(Y);

			//anonymous datatype
			var E1 = new { ID = 101, Name = "Ahmed", Salary = 3000 };
            //compiler made a new anonymous class for you
            Console.WriteLine(E1.GetType().Name); //<>f__AnonymousType0`3
			Console.ReadKey();
			var E2 = new { ID = 102 , Name = "mohamed", Salary = 2000 };
			Console.WriteLine(E2.GetType().Name); //<>f__AnonymousType0`3

			//what if we changed property name OR number of properties OR order of them
			var E3 = new { ID = 102, Name = "mohamed", Salary = 2000, Departmnent= 3 };
			var E4 = new { ID = 102, Name = "mohamed" };
			var E5 = new { ID = 102, Salary = 2000, Name = "mohamed" };
			//for E3 , E4 , E5 all of these have different Anonymous class from each other

			//to be at the same anonyumous class you should have same attributes with same order 


		}
	}
}
