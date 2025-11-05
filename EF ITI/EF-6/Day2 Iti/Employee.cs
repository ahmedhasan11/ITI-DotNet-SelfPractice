using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Iti
{
	internal class Employee
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get;  set; }

		[Column("FullName")]
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		
		public double? Salary { get; set; } //? means that it allows Null

		public string Address { get; set; }

		[Column(TypeName ="Date")]
		public DateTime Date { get; set; }

		[ForeignKey("Dept")] //b e5tsar ana 3ayz a2ol en el FK da by follow el nav property de
		public int DepartmentID { get; set; }
		public virtual Department Dept { get; set; } // 1 employee work in 1 department

		[ForeignKey("SupervisedDept")]
		public int? SupervisedDeptID { get; set; }
		public virtual Department SupervisedDept { get; set; } //1 Employee supervise 1 Department

		public virtual ICollection<Project> Projects { get; set; }
		//employee works f aktr mn project

		//public virtual ICollection<WorksFor> WorksFors { get; set; }
	}
}
