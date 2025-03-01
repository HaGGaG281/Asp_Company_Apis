using Asp_Company_Application.DTO;
using Asp_Company_Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.MappingProfiles
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name)) 
            .ReverseMap();
            CreateMap<Employee, addEmployeeDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, addDepartmentDto>().ReverseMap();
            CreateMap<DepartmentDto, addDepartmentDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<EmployeeProject, EmployeeProjectDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))  // تضمين اسم الموظف
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Title))    // تضمين اسم المشروع
            .ReverseMap();
            CreateMap<addEmployeeProjectDto, EmployeeProject>().ReverseMap();  // إضافة ربط بين الـ DTO والـ Entity

        }

    }
}
