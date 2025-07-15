using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.TaskUseCases
{
    public class DeleteTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryRepository _taskHistoryRepository;

        public DeleteTaskUseCase(ITaskRepository taskRepository, ITaskHistoryRepository taskHistoryRepository)
        {
            _taskRepository = taskRepository;
            _taskHistoryRepository = taskHistoryRepository;
        }

        public async Task ExecuteAsync(int taskId, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            var history = new TaskHistory
            {
                ProjectTaskId = task.Project.Id,
                ChangedByUserId = userId.ToString(),
                ChangedAt = DateTime.UtcNow,
                ChangeDescription = "Tarefa removida",
                ProjectTask = task
            };

            await _taskHistoryRepository.AddAsync(history);

            await _taskRepository.DeleteAsync(task);
        }
    }
}
