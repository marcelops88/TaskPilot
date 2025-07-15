using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.ProjectUseCases
{
    public class CreateProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> ExecuteAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _projectRepository.AddAsync(project);

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            };
        }
    }
}
