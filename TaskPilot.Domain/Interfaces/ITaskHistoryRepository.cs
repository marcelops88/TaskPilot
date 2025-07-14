using TaskPilot.Domain.Entities;

namespace TaskPilot.Domain.Interfaces
{
    public interface ITaskHistoryRepository
    {
        Task AddAsync(TaskHistory history);
    }
}
