using Asp_Company_Application.DTO;
using Asp_Company_Application.Interfaces;
using Asp_Company_Core.Entities;
using Asp_Company_Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp_Company_Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<DepartmentDto>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        
        public async Task<DepartmentDto> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            return department != null ? _mapper.Map<DepartmentDto>(department) : null;
        }


        public async Task<DepartmentDto> AddDepartment(addDepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> UpdateDepartment(int id , addDepartmentDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return null;

            _mapper.Map(departmentDto, department);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return null;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
