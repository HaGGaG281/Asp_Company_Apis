using Microsoft.AspNetCore.Mvc;
using Asp_Company_Core.Entities;
using System.Threading.Tasks;
using Asp_Company_Application.Interfaces;
using Asp_Company_Application.DTO;

namespace Asp_Company_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto registrationDto)
        {
            if (registrationDto == null)
                return BadRequest("Invalid data.");

            var user = await _userRepository.RegisterAsync(registrationDto);
            if (user == null)
                return BadRequest("Registration failed.");

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Invalid data.");

            var token = await _userRepository.LoginAsync(loginDto);
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid username or password.");

            return Ok(new { Token = token });
        }
    }
}