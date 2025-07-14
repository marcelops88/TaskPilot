using TaskPilot.Domain.Entities;

namespace TaskPilot.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<ProjectTask?> GetByIdAsync(int id);
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId);
        Task<int> CountByProjectIdAsync(int projectId);
        Task AddAsync(ProjectTask task);
        Task UpdateAsync(ProjectTask task);
        Task DeleteAsync(ProjectTask task);
    }

}
