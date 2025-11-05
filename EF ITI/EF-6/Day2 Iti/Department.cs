using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Iti
{
	[Table("Department", Schema ="HR")]
	internal class Department
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public string Location { get; set; }

		[InverseProperty("Dept")]
		public virtual ICollection<Employee> Employees { get; set; } //1 department can have many employees
		[InverseProperty("SupervisedDept")]
		public virtual ICollection<Employee> Supervisors { get; set; } //1 Department can have many Supervisors
		public virtual ICollection<Project> Projects { get; set; }
		//notice that we didn't make a DBset for Project but despite we 
		//did that ,, context have Dbset for Department ,, f el context ra7 y4of 
		//el structure bta3 Department ,,, f el context found Collection of project
		//so he found out that there is a relation between department and this class of project 
		//so the context made a table for Project


		//the so big problem here that if you need to access the project 
		//is to make an object from Department


	}
}
