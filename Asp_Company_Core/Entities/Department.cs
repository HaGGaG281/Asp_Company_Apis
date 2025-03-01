using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Core.Entities
{
    public class Department
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employee { get; set; } = new List<Employee>();

    }
}
