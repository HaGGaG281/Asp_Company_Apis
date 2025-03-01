using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>(); 

    }
}
