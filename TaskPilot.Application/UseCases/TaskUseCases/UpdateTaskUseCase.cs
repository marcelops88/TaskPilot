using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.TaskUseCases
{
    public class UpdateTaskUseCase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ITaskHistoryRepository _historyRepo;

        public UpdateTaskUseCase(ITaskRepository taskRepo, ITaskHistoryRepository historyRepo)
        {
            _taskRepo = taskRepo;
            _historyRepo = historyRepo;
        }

        public async Task ExecuteAsync(int projectId, int taskId, UpdateTaskDto dto, string userId)
        {
            var existing = await _taskRepo.GetByIdAsync(taskId);
            if (existing == null || existing.Project.Id != projectId)
                throw new KeyNotFoundException("Tarefa não encontrada ou projeto inválido.");

            var oldStatus = existing.Status;

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            if (dto.DueDate.HasValue)
            {
                existing.DueDate = dto.DueDate.Value;
            }
            if (dto.Status.HasValue)
            {
                existing.Status = (ProjectTaskStatus)dto.Status.Value;
            }

            await _taskRepo.UpdateAsync(existing);

            var history = new TaskHistory
            {
                ProjectTaskId = existing.Project.Id,
                ChangeDescription = "Tarefa atualizada",
                ChangedByUserId = userId,
                ChangedAt = DateTime.UtcNow
            };

            await _historyRepo.AddAsync(history);
        }
    }
}
