using Asp_Company_Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp_Company_Application.Interfaces
{
    public interface IEmployeeProjectRepository
    {
        Task<IEnumerable<EmployeeProjectDto>> GetEmployeeProjects(int empId);
        Task<IEnumerable<EmployeeProjectDto>> GetEmployeesProjects();
        Task<IEnumerable<ProjectEmployeeDto>> GetProjectEmployees(int projectId);
        Task<addEmployeeProjectDto> AddEmployeeProject(addEmployeeProjectDto employeeProjectDto);
        Task<addEmployeeProjectDto> UpdateEmployeeProject(int prjId,int empId,addEmployeeProjectDto employeeProjectDto);
        Task<bool> DeleteEmployeeProject(int empId, int projectId);
    }
}
