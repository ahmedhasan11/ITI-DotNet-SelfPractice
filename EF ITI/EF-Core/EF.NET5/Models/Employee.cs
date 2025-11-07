using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.NET5.Models
{
    internal class Employee
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        //notice that we didnt make a data annotation attributre because by convention
        //by convention context detected that the column name is DepartmentID (EntitynameID)

        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
