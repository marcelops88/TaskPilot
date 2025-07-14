using Microsoft.AspNetCore.Mvc;
using TaskPilot.Application.Dtos;

namespace TaskPilot.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Lista todos os projetos do usuário.
        /// </summary>
        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = new List<ProjectDto>
            {
                new ProjectDto { Id = 1, Name = "Projeto Exemplo", Description = "Descrição do projeto" }
            };

            return Ok(projects);
        }

        /// <summary>
        /// Cria um novo projeto.
        /// </summary>
        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectDto dto)
        {
            var createdProject = new ProjectDto
            {
                Id = 1,
                Name = dto.Name,
                Description = dto.Description
            };

            return CreatedAtAction(nameof(GetProjects), new { id = createdProject.Id }, createdProject);
        }

        /// <summary>
        /// Atualiza um projeto.
        /// </summary>
        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] UpdateProjectDto dto)
        {

            return NoContent();
        }

        /// <summary>
        /// Remove um projeto.
        /// </summary>
        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {

            return NoContent();
        }
    }
}
