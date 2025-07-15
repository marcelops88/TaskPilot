using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.TaskUseCases
{
    public class CreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ITaskHistoryRepository _historyRepo;

        public CreateTaskUseCase(ITaskRepository taskRepo, IProjectRepository projectRepo, ITaskHistoryRepository historyRepo)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _historyRepo = historyRepo;
        }

        public async Task<TaskDto> ExecuteAsync(int projectId, CreateTaskDto dto, string userId)
        {
            var project = await _projectRepo.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            var taskCount = project.ProjectTask.Count;
            if (taskCount >= 20)
                throw new InvalidOperationException("Limite máximo de 20 tarefas por projeto atingido.");

            if (!dto.DueDate.HasValue)
                throw new ArgumentException("A data de vencimento é obrigatória.", nameof(dto.DueDate));

            var task = new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate.Value,
                Status = ProjectTaskStatus.Pendente,
                Priority = dto.Priority,
                Project = project
            };

            await _taskRepo.AddAsync(task);

            var history = CreateHistory(task, "Tarefa criada", userId);
            await _historyRepo.AddAsync(history);

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority
            };
        }

        private TaskHistory CreateHistory(ProjectTask task, string description, string userId)
        {
            return new TaskHistory
            {
                ProjectTaskId = task.Id,
                ChangeDescription = description,
                ChangedByUserId = userId,
                ChangedAt = DateTime.UtcNow,
                ProjectTask = task
            };
        }
    }
}
