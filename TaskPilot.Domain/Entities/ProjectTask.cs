using TaskPilot.Domain.Enums;

namespace TaskPilot.Domain.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public Priority Priority { get; set; }
        public Project Project { get; set; } = null!;
        public List<TaskComment> Comments { get; set; } = new();
    }
}
