using Asp_Company_Application.DTO;
using Asp_Company_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetDepartments();
        Task<DepartmentDto> GetDepartment(int id);
        Task<DepartmentDto> AddDepartment(addDepartmentDto department);
        Task<DepartmentDto> UpdateDepartment(int id , addDepartmentDto department);
        Task<DepartmentDto> DeleteDepartment(int id);
    }
}
