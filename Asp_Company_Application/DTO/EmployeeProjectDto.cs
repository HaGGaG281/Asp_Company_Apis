using Asp_Company_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.DTO
{
    public class EmployeeProjectDto
    {
        public string EmployeeName { get; set; } // اسم الموظف
        public string ProjectName { get; set; }  // اسم المشروع
    }
}
