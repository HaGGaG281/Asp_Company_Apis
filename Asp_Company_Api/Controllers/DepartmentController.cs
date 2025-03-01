using Asp_Company_Application.DTO;
using Asp_Company_Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp_Company_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _departmentRepository.GetDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartment(id);
            if (department == null)
                return NotFound("Department not found");

            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> AddDepartment([FromBody] addDepartmentDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest("Invalid department data");

            var createdDepartment = await _departmentRepository.AddDepartment(departmentDto);
            return Ok(createdDepartment);
        }

        [HttpPut]
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment([FromQuery]int id, [FromBody] addDepartmentDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest("Invalid department data");

            var updatedDepartment = await _departmentRepository.UpdateDepartment(id ,departmentDto);
            if (updatedDepartment == null)
                return NotFound("Department not found");

            return Ok(updatedDepartment);
        }

        [HttpDelete]
        public async Task<ActionResult<DepartmentDto>> DeleteDepartment([FromQuery]int id)
        {
            var deletedDepartment = await _departmentRepository.DeleteDepartment(id);
            if (deletedDepartment == null)
                return NotFound("Department not found");

            return Ok(deletedDepartment);
        }
    }
}
