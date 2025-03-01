using Asp_Company_Application.DTO;
using Asp_Company_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.Interfaces
{
    public interface IEmployeeRepository 
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployee(int id);
        Task<addEmployeeDto> AddEmployee(addEmployeeDto employee);
        Task<addEmployeeDto> UpdateEmployee(int id, addEmployeeDto employee);
        Task<bool> DeleteEmployee(int id);
        //Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentIdAsync(int departmentId);

    }
}
