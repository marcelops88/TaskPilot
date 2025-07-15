using TaskPilot.Domain.Enums;

namespace TaskPilot.Application.Dtos
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public ProjectTaskStatus? Status { get; set; }
    }
}
