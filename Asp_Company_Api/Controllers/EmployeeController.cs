using Asp_Company_Api.HelperFunction;
using Asp_Company_Application.DTO;
using Asp_Company_Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Company_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            //var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //if (string.IsNullOrEmpty(token))
            //{
            //    return Unauthorized("Token is missing");
            //}

            //var customerId = ExtractClaims.ExtractUserIdFromToken(token);

            //if (!customerId.HasValue)
            //{
            //    return Unauthorized("Invalid or missing user ID in token.");
            //}
            var employees = await _employeeRepository.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployee(id);
            if (employee == null)
                return NotFound($"Employee with id  {id} not found.");

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<addEmployeeDto>> AddEmployee([FromBody] addEmployeeDto employeeDto)
        {
            //var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //if (string.IsNullOrEmpty(token))
            //{
            //    return Unauthorized("Token is missing");
            //}

            //var customerId = ExtractClaims.ExtractUserIdFromToken(token);

            //if (!customerId.HasValue)
            //{
            //    return Unauthorized("Invalid or missing user ID in token.");
            //}

            if (employeeDto == null)
                return BadRequest("Employee data is null.");

            var createdEmployee = await _employeeRepository.AddEmployee(employeeDto);
            return Ok(createdEmployee);
        }

        [HttpPut]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromQuery]int id, [FromBody] addEmployeeDto employeeDto)
        {

            var updatedEmployee = await _employeeRepository.UpdateEmployee(id,employeeDto);
            if(updatedEmployee == null)
                return NotFound($"Employee with id {id} not found.");
            return Ok(updatedEmployee);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee([FromQuery]int id)
        {
            var deleted = await _employeeRepository.DeleteEmployee(id);
            if (!deleted)
                return NotFound($"Employee with id {id} not found.");

            return NoContent(); 
        }

    }
}
