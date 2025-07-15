namespace TaskPilot.Application.Dtos
{
    public class TaskCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
