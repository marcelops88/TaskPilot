using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.ProjectUseCases
{
    public class DeleteProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public DeleteProjectUseCase(IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            var pendingTasks = await _taskRepository.GetPendingTasksByProjectIdAsync(projectId);

            if (pendingTasks.Any())
            {
                throw new InvalidOperationException(
                    "Não é possível remover o projeto. Existem tarefas pendentes associadas. Conclua ou remova as tarefas primeiro."
                );
            }

            await _projectRepository.DeleteAsync(project);
        }
    }
}
