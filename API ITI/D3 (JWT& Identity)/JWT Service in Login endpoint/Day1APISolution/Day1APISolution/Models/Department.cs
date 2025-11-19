using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Day1APISolution.Models
{
	public class Department
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string? ManagerName { get; set; }
		[JsonIgnore]//to ignore serialize and deserialize that happens in consuming f
		//from .net apps
		public List<Employee>? Emps { get; set; }
	}
}
