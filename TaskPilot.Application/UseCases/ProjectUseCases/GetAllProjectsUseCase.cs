using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.ProjectUseCases
{
    public class GetAllProjectsUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> ExecuteAsync()
        {
            var projects = await _projectRepository.GetAllAsync();

            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            });
        }
    }
}
