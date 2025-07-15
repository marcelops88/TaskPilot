using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPilot.Domain.Entities
{
    [NotMapped]
    public class UserPerformanceData
    {
        public int UserId { get; set; }
        public int CompletedTasks { get; set; }
    }

}
