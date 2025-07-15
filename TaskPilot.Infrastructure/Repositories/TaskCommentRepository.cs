using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Infrastructure.Context;

namespace TaskPilot.Infrastructure.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly AppDbContext _context;

        public TaskCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskComment comment)
        {
            await _context.TaskComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}
