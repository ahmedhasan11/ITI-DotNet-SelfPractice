using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Iti
{
	internal class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual Department Department { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }
		//el project by4t8l feh aktr mn eployee

		//public virtual ICollection<WorksFor> WorksFors { get; set; }
	}
}
