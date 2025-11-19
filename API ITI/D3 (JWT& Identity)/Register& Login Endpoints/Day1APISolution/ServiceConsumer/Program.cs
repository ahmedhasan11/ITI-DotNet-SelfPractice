using System.Net.Http.Json;

namespace ServiceConsumer
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			HttpClient client = new HttpClient();
			Dept d =await client.GetFromJsonAsync<Dept>("http://localhost:5046/api/department/1");
			Console.WriteLine(d.Name);
		}
	}
}
