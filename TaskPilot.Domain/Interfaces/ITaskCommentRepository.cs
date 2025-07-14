using TaskPilot.Domain.Entities;

namespace TaskPilot.Domain.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task AddAsync(TaskComment comment);
    }
}
