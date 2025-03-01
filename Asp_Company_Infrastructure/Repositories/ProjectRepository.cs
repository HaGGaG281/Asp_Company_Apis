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
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProject(int Id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == Id);
            return project != null ? _mapper.Map<ProjectDto>(project) : null;
        }

        public async Task<ProjectDto> AddProject(addProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> UpdateProject(int id, addProjectDto projectDto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
                return null;

            _mapper.Map(projectDto, project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> DeleteProject(int Id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == Id);
            if (project == null)
                return null;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }
    }
}
