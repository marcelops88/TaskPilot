namespace TaskPilot.Domain.Entities
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int ProjectTaskId { get; set; }
        public string ChangedByUserId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangeDescription { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
