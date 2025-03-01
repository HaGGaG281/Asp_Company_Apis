using Asp_Company_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Asp_Company_Application.Interfaces;
using Asp_Company_Infrastructure.Data;
using Asp_Company_Application.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Asp_Company_Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // تسجيل المستخدم
        public async Task<User?> RegisterAsync(UserDto registrationDto)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == registrationDto.Username);
            if (existingUser != null)
                return null;

            // Hash the password
            var hashedPassword = HashPassword(registrationDto.Password);

            // Create new User
            var user = new User
            {
                Username = registrationDto.Username,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // تسجيل الدخول
        public async Task<string> LoginAsync(UserDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || !VerifyPassword(loginDto.Password, user.Password))
                return string.Empty;

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return token;
        }

        // تجزئة كلمة المرور (Hash Password)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        // التحقق من كلمة المرور
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            var inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedHash;
        }

        // إنشاء الـ JWT Token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
