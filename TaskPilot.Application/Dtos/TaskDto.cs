using TaskPilot.Domain.Enums;

namespace TaskPilot.Application.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public Priority Priority { get; set; }
        public List<TaskCommentDto>? Comments { get; set; }
    }
}
