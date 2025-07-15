using Microsoft.AspNetCore.Mvc;
using TaskPilot.Application.Dtos;
using TaskPilot.Application.UseCases.ProjectUseCases;

namespace TaskPilot.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly CreateProjectUseCase _createProjectUseCase;
        private readonly UpdateProjectUseCase _updateProjectUseCase;
        private readonly GetAllProjectsUseCase _getAllProjectsUseCase;
        private readonly GetProjectByIdUseCase _getProjectByIdUseCase;
        private readonly DeleteProjectUseCase _deleteProjectUseCase;

        public ProjectsController(
            CreateProjectUseCase createProjectUseCase,
            UpdateProjectUseCase updateProjectUseCase,
            GetAllProjectsUseCase getAllProjectsUseCase,
            GetProjectByIdUseCase getProjectByIdUseCase,
            DeleteProjectUseCase deleteProjectUseCase)
        {
            _createProjectUseCase = createProjectUseCase;
            _updateProjectUseCase = updateProjectUseCase;
            _getAllProjectsUseCase = getAllProjectsUseCase;
            _getProjectByIdUseCase = getProjectByIdUseCase;
            _deleteProjectUseCase = deleteProjectUseCase;
        }

        /// <summary>
        /// Lista todos os projetos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _getAllProjectsUseCase.ExecuteAsync();
            return Ok(projects);
        }

        /// <summary>
        /// Cria um novo projeto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var createdProject = await _createProjectUseCase.ExecuteAsync(dto);

            return CreatedAtAction(nameof(GetProjects), new { id = createdProject.Id }, createdProject);
        }

        /// <summary>
        /// Atualiza um projeto.
        /// </summary>
        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, [FromBody] UpdateProjectDto dto)
        {
            var updatedProject = await _updateProjectUseCase.ExecuteAsync(projectId, dto);

            return Ok(updatedProject);
        }

        /// <summary>
        /// Remove um projeto.
        /// </summary>
        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            await _deleteProjectUseCase.ExecuteAsync(projectId);

            return NoContent();
        }
    }
}
