using Asp_Company_Application.DTO;
using Asp_Company_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp_Company_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public EmployeeProjectController(IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
        }

        [HttpGet("employee_projects/{empId}")]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDto>>> GetEmployeeProjects(int empId)
        {
            var employeeProjects = await _employeeProjectRepository.GetEmployeeProjects(empId);
            return Ok(employeeProjects);
        }

        [HttpGet("employee_projects")]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDto>>> GetEmployeesProjects()
        {
            var employeeProjects = await _employeeProjectRepository.GetEmployeesProjects();
            return Ok(employeeProjects);
        }


        [HttpGet("projects_employees/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectEmployeeDto>>> GetProjectEmployees(int projectId)
        {
            var projectEmployees = await _employeeProjectRepository.GetProjectEmployees(projectId);
            return Ok(projectEmployees);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeProjectDto>> AddEmployeeProject([FromBody] addEmployeeProjectDto employeeProjectDto)
        {
            if (employeeProjectDto == null)
                return BadRequest("Invalid data.");

            var createdEmployeeProject = await _employeeProjectRepository.AddEmployeeProject(employeeProjectDto);
            return CreatedAtAction(nameof(GetEmployeeProjects), new { empId = createdEmployeeProject.EmployeeId }, createdEmployeeProject);
        }

        [HttpPut("{empid}/{prjId}")]
        public async Task<ActionResult<EmployeeProjectDto>> UpdateEmployeeProject(int prjId, int empid, [FromBody] addEmployeeProjectDto employeeProjectDto)
        {
            if (employeeProjectDto == null)
                return BadRequest("Invalid data.");

            var updatedEmployeeProject = await _employeeProjectRepository.UpdateEmployeeProject(prjId,empid ,employeeProjectDto);
            if (updatedEmployeeProject == null)
                return NotFound("Employee project not found.");

            return Ok(updatedEmployeeProject);
        }

        [HttpDelete("{empId}/{projectId}")]
        public async Task<ActionResult<EmployeeProjectDto>> DeleteEmployeeProject(int empId, int projectId)
        {
            var deletedEmployeeProject = await _employeeProjectRepository.DeleteEmployeeProject(empId, projectId);
            if (deletedEmployeeProject == null)
                return NotFound("Employee project not found.");

            return Ok(deletedEmployeeProject);
        }
    }
}
