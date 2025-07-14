namespace TaskPilot.Domain.Entities
{
    public class TaskComment
    {
        public int Id { get; set; }
        public int ProjectTaskId { get; set; }
        public string UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
