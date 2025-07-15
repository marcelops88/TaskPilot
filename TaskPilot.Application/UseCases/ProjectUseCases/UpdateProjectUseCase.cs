using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.ProjectUseCases
{
    public class UpdateProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> ExecuteAsync(int projectId, UpdateProjectDto dto)
        {
            var existing = await _projectRepository.GetByIdAsync(projectId);
            if (existing == null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            await _projectRepository.UpdateAsync(existing);

            return new ProjectDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Description = existing.Description
            };
        }
    }
}
