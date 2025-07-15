using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.ProjectUseCases
{
    public class GetProjectByIdUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> ExecuteAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            return project;
        }
    }
}
