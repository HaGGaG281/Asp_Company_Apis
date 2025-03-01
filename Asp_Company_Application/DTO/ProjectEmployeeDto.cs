using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.DTO
{
    public class ProjectEmployeeDto
    {
        public string ProjectName { get; set; }  // اسم المشروع
        public List<string> EmployeeNames { get; set; }  // قائمة بأسماء الموظفين
    }
}
