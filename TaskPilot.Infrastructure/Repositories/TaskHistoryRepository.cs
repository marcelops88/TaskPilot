using Microsoft.EntityFrameworkCore;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Infrastructure.Context;

namespace TaskPilot.Infrastructure.Repositories
{
    public class TaskHistoryRepository : ITaskHistoryRepository
    {
        private readonly AppDbContext _context;

        public TaskHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskHistory history)
        {
            await _context.TaskHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserPerformanceData>> GetCompletedTasksCountPerUserAsync(DateTime fromDate)
        {
            var result = await _context.TaskHistories
                .Where(h => h.ProjectTask.Status == Domain.Enums.ProjectTaskStatus.Concluida && h.ChangedAt >= fromDate)
                .GroupBy(h => h.ChangedByUserId)
                .Select(g => new UserPerformanceData
                {
                    UserId = g.Key != null ? ParseUserId(g.Key) : 0,
                    CompletedTasks = g.Select(x => x.ProjectTaskId).Distinct().Count()
                })
                .ToListAsync();

            return result;
        }

        private int ParseUserId(string userId)
        {
            return int.TryParse(userId, out var id) ? id : 0;
        }
    }
}
