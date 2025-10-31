using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Linq_pt2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> lst = new List<int>() { 1 ,2,3,4,5};

		  //1-	//static function member in enumerable class
			var linq1= Enumerable.Where(lst, i => i % 2 == 0);

		  //2-	//extension method
		 var linq2=	lst.Where(i => i % 2==0);


		  //3-	//query syntax , query expression
			var linq3 = from i in lst
						where i % 2 == 0
						select i;

		//1,2,3 does same code and have same execution , (translated to the same execution from the compiler)



		}
	}
}
