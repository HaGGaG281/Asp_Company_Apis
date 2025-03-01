using Asp_Company_Application.DTO;
using Asp_Company_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp_Company_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _projectRepository.GetProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var project = await _projectRepository.GetProject(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> AddProject([FromBody] addProjectDto projectDto)
        {
            if (projectDto == null)
                return BadRequest("Project data is invalid.");

            var createdProject = await _projectRepository.AddProject(projectDto);
            return CreatedAtAction(nameof(GetProject), new { id = createdProject.Title }, createdProject);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> UpdateProject([FromQuery] int id, [FromBody] addProjectDto projectDto)
        {
            if (projectDto == null)
                return BadRequest("Project data is invalid.");

            //var existingProject = await _projectRepository.GetProject(id);
            //if (existingProject == null)
            //    return NotFound($"Project with ID {id} not found.");

            var updatedProject = await _projectRepository.UpdateProject(id ,projectDto);
            return Ok(updatedProject);
        }

        [HttpDelete]
        public async Task<ActionResult<ProjectDto>> DeleteProject([FromQuery] int id)
        {
            var project = await _projectRepository.DeleteProject(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            return Ok(project);
        }
    }
}
