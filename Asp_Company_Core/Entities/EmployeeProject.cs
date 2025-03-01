using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Core.Entities
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; } // Foreign Key
        public Employee Employee { get; set; } // Navigation Property

        public int ProjectId { get; set; } // Foreign Key
        public Project Project { get; set; } // Navigation Property
    }
}
