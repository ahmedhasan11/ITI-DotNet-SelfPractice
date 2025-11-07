using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.NET5.Models
{
    internal class Branch
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
