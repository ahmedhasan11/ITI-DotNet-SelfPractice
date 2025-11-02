using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LINQ_day_3
{
	internal class Program
	{
		static void Main(string[] args)
		{

			//in linq while using where , you can make an index that make more filteration to select some of the rows
			//like selecting top 5 rows of your condition,

			List<int> lst = new List<int>() { 1,2,3,4,5};
			//indexed where , vaild only in that syntax
			var result = lst.Where((p, i) => p % 2 == 0 && i < 5);



			 #region Select : transformation
			//1- Product => String
			var Result = ProductList.Select(p => p.ProductName);
					//another way
			result = from p in productList
					 select p.ProductName;
			
			//2-Product => Anonymous Type
			var result = productlist.where(p => p.UnitsinStock == 0)
								.Select(p => new { id = p.productId, p.productName });
					//another way
			result = from p in productlist
					 select new { id = p.productId, p.productName };

			//3-Product => Product
			var discountList = productlist.select(p => new Product()
			{
				productid = p.productid,
				CategoryAttribute=p.category,
				productname=p.productname,
				unitprice=p.unitprice
			});
					//another way
			discountedlist=from p in productlist
						   selectr new product()
						   {
							   productid = p.productid,
							   CategoryAttribute = p.category,
							   productname = p.productname,
							   unitprice = p.unitprice
						   };


			//indexed select
			var result = productlist.where(p => p.UnitsinStock == 0)
				.Select((p, i) => new { index = i, name = p.product });
			//what if i want to make select first then do the where clause?
			//we have two ways , 1- using fluent syntax
			var Result = prdlist.select(p => new { name = propa.name, newprice = propa.unitprice * 0.9 })
				.where(p => p.newprice > 20); //norice that you can make this in 2 setatments easier

			//second way : using query syntax
			//here you should make it in two steps
			var RR01 =from p in prdlist
					  select new {name=p.name,newprice=p.unitprice*0.9 }

			var RR02 = from p in RR01 //notice in RR01
					   where p.newprice > 20
					   select p;

			//but what if you want to do the same code in above in one sentence???
			//here we goes advanced , we will make a dictionary
			//that dictionary we can put first query in it

			var result = from p in prdlist
						 select new { name = p.name, newprice = p.unitprice * 0.9 }
						 into TaxedList //introduced new range variable to continue query using that new range variables
						 where TaxedList.newprice > 20
						 select TaxedList;
			#endregion

			#region Ordering
			//by default it order as ascending
			var result = productlist.OrderBy(p => p.UnitInStock);
					.select(p => new {p.productname , p.unitinstock })

			result = from p in productlist
					 orderby p.unitInStock
					 select new {P.productname, P.Unitinstock};

			//descending
				//1-first way
			var result = productlist.OrderByDescending(p => p.UnitInStock);
					.select(p => new { p.productname, p.unitinstock , p.unitprice})
				//2-second way
			result = from p in productlist
				orderby p.unitInStock descending
				select new {P.productname, P.Unitinstock};

			//what if there is two products have same unit in stock???
			//we here should order in another way other than the first way

			//1-first way
			var result = productlist.OrderByDescending(p => p.UnitInStock)
			.ThenBy(p => p.unitprice) //here then by is ascending
					.select(p => new { p.productname, p.unitinstock,p.unitprice });

			var result = productlist.OrderByDescending(p => p.UnitInStock)
			.ThenByDescending(p => p.unitprice) //here then by is descending
		.select(p => new { p.productname, p.unitinstock, p.unitprice });

			//2-second way

			var result = from p in productlist
						 orderby p.unitinstock descending, p.unitprice //norice that here unitstock is descending, unitprice is ascending
						 select new { p.productname, p.unitinstock };

			var result = from p in productlist
						 orderby p.unitinstock descending, p.unitprice descending 
						 select new { p.productname, p.unitinstock };


			//mn el a5r
			// in fluent expression:
			//you have : OrderBy , OrderByDescending , ThenBy , ThenByDescending




			//in query syntax OR query Expression
			//you have : OrderyBy , OrderBy p.unitstock Descending ,,,,,,
			//and if you want to make another filteration you can easily add comma


			#endregion

			#region Element Operators :valid only in Fluent Syntax

			//returns single element
			// First ,Last ,FirstOrDefault,LastOrDefault,ElementAt, ElementAtOrDefault

			List<product> discountedlist = new List<product>();
			List<product> productlist = new List<product>();


//First
			var result3 = productlist.First(); //returns first element of the list 
											   //if list is empty , exception thrown

			var result5 = productlist.First(p => p.unitprice == 0); //same situation as above , expception thrown

//FirstOrDefault
			var result5 = productlist.FirstOrDefault(); //returns first element of the list
														//if list is empty , returns default value which is "Null" in our situation , so no exception thrown

			var result5 = productlist.FirstOrDefault(p=> p.unitprice==0); //same situation as above , returns default if condition not satisfied



//Last
			var result4 = productlist.Last(); //returns last element of the list
											  //if list is empty , exception thrown

			var result5 = productlist.Last(p => p.unitprice == 0); //same situation as above , expception thrown


//LastOrDefault
			var result6 = productlist.LastOrDefault();//returns Last element of the list
													  //if list is empty , returns default value which is "Null" in our situation , so no exception thrown

			var result5 = productlist.LastOrDefault(p => p.unitprice == 0); //same situation as above , returns default if condition not satisfied

//in above we said that these are only valid in Fluent syntax

//what if we want to do a query syntax && in same way we need to use First

//answer to above question is to use Hybrid Syntax 
//Hybrid Syntax : (Query Expression).Fluent Syntax

			var result = (from p in productlist
						  where p.unitprice ==0
						  select p).First();

//ElementAt(index)

			var result = productlist.ElementAt(17); //throw exception if index out of range

//ElementAtOrDefault(index)

			var result = productlist.ElementAtOrDefault(17); //returns default if index out of range



			#endregion

			#region Aggregate: Count, Max, Min, Average, Sum
			//Count(), Count(predicate)
			var result= productlist.Count(); 

			var result = productlist.Count(p=>p.unitinstock==0);



//hna b2a hnbd2 el 7agat el s3ba ,, feh function esmha Max() 5lena n4of elle hy7sl
//Max()
			var result = productlist.Max(); //exception thrown , C# bt4of enk m4 mdeha ay 7aga t
											//tcompare 3leha , f ht2olk e3ml class product w edelo "IComparable<Product>"
											//w kman ht5lek t3ml function gwa el class product da t5leha t compare

			//kol elle 2olnah fo2 da 7sl bsbb enk m4 mde predicate gwa function el max


//Max(predicate)
			var result = productlist.Max(p=>p.unitprice); //returns max price only 
														  //w hna b2a m4 m7tag t3ml ay 7aga mn el m4akl elle 7slt 3shan enta edeto predicate

//Min()
			var result = productlist.Min(); //exception thrown , C# bt4of enk m4 mdeha ay 7aga t
											//tcompare 3leha , f ht2olk e3ml class product w edelo "IComparable<Product>"
											//w kman ht5lek t3ml function gwa el class product da t5leha t compare

			//kol elle 2olnah fo2 da 7sl bsbb enk m4 mde predicate gwa function el max


//Min(predicate)
			var result = productlist.Min(p=>p.unitprice);

//Average(predicate)
			var result = productlist.Average(p => p.unitinstock);

//Sum(predicate)
			var result = productlist.Sum(p => p.unitinstock);


			#endregion

			#region Generators Operators: Enumerable.Empty,Range,Repeat
			//generates only output sequence
			//only way to call is as static members from enumerable class

			var res = Enumerable.Range(0,10); //hy3ml squence bybd2 mn awl 0 w hy3d 10 mrat

			var res = Enumerable.Empty<product>();//hy3ml sequence empty

			var res = Enumerable.Repeat(3, 10); //print "3" 10 times

			var result = Enumerable.Repeat(productlist[2], 3); //print full object of "productlist[2]" 3 times




			#endregion

			#region Select Many
			//output seq count > input seq count
			List<string> NameList= new List<string>() {"mahmoud ali","mohamed samit","ahmed hasan" };
			//fluent syntax
			var result = NameList.SelectMany(FN => FN.Split(' '));
			//query expression
			var result = from FN in NameList
						 from SN in FN.Split(' ')
						 select SN;


			#endregion

			#region Casting Operators:ToList,ToArray,ToDictionary,ToHashSet,ToLookup

			//ToList,ToArray,ToDictionary,ToHashSet,ToLookup

			List<product> UNavailableProducts = productlist.Where(p => p.unitinstock == 0);
			//here there is an error , elle 3la el shmal List<product>
			//elle 3la el ymen Ienumerable<product>
			//hna b2a htegy el casting operators


			List<product> UNavailableProduct = productlist.Where(p => p.unitinstock == 0).ToList();
			//kda b2a 3ndk el ymen w el shmal 3bara 3n list<product>

			var result1 = productlist.Where(p => p.unitinstock == 0)
							.ToHashSet();
			//Dic<long,product>	        .ToDictionary(product => product.productID);
			//Dic<long,Anonymous[long,string]>	.ToDictionary(product=>product.productID, Prd => new {prd.productID,prd.ProductName});




			#endregion

			#region Set Operators :Union, Intersect, Except, Concat, Distinct

			var seq1 = Enumerable.Range(0, 50); //mn 0 : 49
			var seq2 = Enumerable.Range(50, 150); //mn 50 : 149

			var result = seq1.Union(seq2); //removes duplicates

			var result= seq1.Intersect(seq2); 

			var result = seq1.Except(seq2); //set difference


			//Concat + Distinct = Union
			var result77= seq1.Concat(seq2); // if you dont want to remove duplicate
			var result = result77.Distinct(); //if you made concat and need to remove duplicates
			#endregion

			#region Quantifiers : returns boolean : Any , All , SequenceEqual

			productlist.Any(); //returns true if list have at least one element
			productlist.Any(p=>p.unitprice > 3000); //returns true if any element matches

			productlist.All(p=>p.unitinstock!=0); //returns true if all elements matches

			seq1.SequenceEqual(seq2); // returns true if each and every element with same order = seq2



			#endregion

			#region Zip
			//input 2 seq , one combined output seq

			var lst02 = Enumerable.Range(0, 10);
			var result = NameList.Zip(lst02, (FN, i) => new { i, Name = FN.ToUpper() });


			#endregion

			#region Grouping
			var result = from p in productlist
						 where p.unitinstock > 0
						 group p by p.Category;

			var result = productlist.GroupBy(p => p.productID);


			#endregion

			#region Let \ Into -introducing new range variable in query syntax
			List<string> Names = new List<string>() { "ahmed","mohamed","mahmoud","ali","menna","haneen"};

//Into
			var result = from N in Names
						 select Regex.Replace(N, "[aoieuAOIEU]", string.Empty)
						 into NoVol //restart query with new range variables
									//while old range variables not accessable
						 where NoVol.Length >= 3
						 orderby NoVol
						 select NoVol;
						
//Let
			var result= from N in Names
						let NoVol= Regex.Replace(N, "[aoieuAOIEU]", string.Empty)
						//Let : Continue query with added range variables
						where NoVol.Length >= 3
						orderby NoVol , N.Length //here i can access the old raviable
						select NoVol;
			#endregion


		}
	}
}
