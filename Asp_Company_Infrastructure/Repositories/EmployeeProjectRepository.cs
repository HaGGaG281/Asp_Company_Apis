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
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeProjectRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeProjectDto>> GetEmployeeProjects(int empId)
        {
            var employeeProjects = await _context.EmployeeProject
            .Where(ep => ep.EmployeeId == empId)
            .Include(ep => ep.Employee) // تضمين بيانات الموظف
            .Include(ep => ep.Project)  // تضمين بيانات المشروع
            .ToListAsync();

            // استخدام AutoMapper لتحويل الكائنات إلى EmployeeProjectDto
            return _mapper.Map<IEnumerable<EmployeeProjectDto>>(employeeProjects);
        }

        public async Task<IEnumerable<ProjectEmployeeDto>> GetProjectEmployees(int projectId)
        {
            var projectEmployees = await _context.EmployeeProject
            .Where(ep => ep.ProjectId == projectId)
            .Include(ep => ep.Employee) // تضمين بيانات الموظف
            .Include(ep => ep.Project)  // تضمين بيانات المشروع
            .ToListAsync();

            // إنشاء الاستجابة بالشكل المطلوب
            var projectEmployeeDto = projectEmployees
                .GroupBy(ep => ep.Project.Title)  // تجميع البيانات حسب اسم المشروع
                .Select(group => new ProjectEmployeeDto
                {
                    ProjectName = group.Key,  // اسم المشروع
                    EmployeeNames = group.Select(ep => ep.Employee.Name).ToList()  // قائمة بأسماء الموظفين
                })
                .ToList();

            return projectEmployeeDto;
        }

        public async Task<addEmployeeProjectDto> AddEmployeeProject(addEmployeeProjectDto employeeProjectDto)
        {
            var employeeProject = _mapper.Map<EmployeeProject>(employeeProjectDto);
            _context.EmployeeProject.Add(employeeProject);
            await _context.SaveChangesAsync();
            return _mapper.Map<addEmployeeProjectDto>(employeeProject);
        }

        public async Task<addEmployeeProjectDto> UpdateEmployeeProject(int prjId,int empId ,addEmployeeProjectDto employeeProjectDto)
        {
            // العثور على السجل الحالي باستخدام EmployeeId و ProjectId
            var employeeProject = await _context.EmployeeProject
                .FirstOrDefaultAsync(ep => ep.EmployeeId == empId && ep.ProjectId == prjId);

            if (employeeProject == null)
                return null;

            // إذا كنت بحاجة إلى تغيير EmployeeId أو ProjectId، يجب حذف السجل القديم
            _context.EmployeeProject.Remove(employeeProject);
            await _context.SaveChangesAsync();

            // الآن إضافة السجل الجديد مع العلاقة الجديدة
            var newEmployeeProject = new EmployeeProject
            {
                EmployeeId = employeeProjectDto.EmployeeId,
                ProjectId = employeeProjectDto.ProjectId
            };

            _context.EmployeeProject.Add(newEmployeeProject);
            await _context.SaveChangesAsync();

            // إعادة خريطة البيانات
            return _mapper.Map<addEmployeeProjectDto>(newEmployeeProject);
        }

        public async Task<bool> DeleteEmployeeProject(int empId, int projectId)
        {
            var employeeProject = await _context.EmployeeProject
                .FirstOrDefaultAsync(ep => ep.EmployeeId == empId && ep.ProjectId == projectId);

            if (employeeProject == null)
                return false;

            _context.EmployeeProject.Remove(employeeProject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
