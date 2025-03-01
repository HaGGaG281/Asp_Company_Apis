using Asp_Company_Application.DTO;
using Asp_Company_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> RegisterAsync(UserDto registrationDto);
        Task<string> LoginAsync(UserDto loginDto);
    }
}
