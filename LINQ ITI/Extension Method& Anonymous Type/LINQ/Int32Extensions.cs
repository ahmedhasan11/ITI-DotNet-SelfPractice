using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
	internal static class Int32Extensions
	{
		public static int Mirror(this int i)
		{
			var Arr = i.ToString().ToCharArray();
			Array.Reverse(Arr);
			return int.Parse(new string(Arr));
		}
	}
}
