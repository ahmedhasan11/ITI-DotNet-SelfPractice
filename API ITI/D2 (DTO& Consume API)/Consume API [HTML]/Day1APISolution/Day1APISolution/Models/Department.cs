namespace Day1APISolution.Models
{
	public class Department
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string? ManagerName { get; set; }

		public List<Employee>? Emps { get; set; }
	}
}
