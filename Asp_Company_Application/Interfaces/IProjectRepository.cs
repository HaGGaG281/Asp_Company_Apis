using Asp_Company_Application.DTO;
using Asp_Company_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Company_Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectDto>> GetProjects();
        Task<ProjectDto> GetProject(int Id);
        Task<ProjectDto> AddProject(addProjectDto project);
        Task<ProjectDto> UpdateProject(int id, addProjectDto project);
        Task<ProjectDto> DeleteProject(int Id);
    }
}
