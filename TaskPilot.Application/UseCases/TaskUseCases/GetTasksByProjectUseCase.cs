using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.TaskUseCases
{
    public class GetTasksByProjectUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksByProjectUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDto>> ExecuteAsync(int projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                ProjectId = t.Project.Id.ToString(),
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Status = t.Status,
                Priority = t.Priority,
                Comments = t.Comments?.Select(c => new TaskCommentDto
                {
                    Id = c.Id,
                    Content = c.CommentText,
                    CreatedAt = c.CreatedAt,
                    UserId = c.UserId
                }).ToList()
            });
        }
    }
}
