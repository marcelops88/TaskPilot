using Microsoft.EntityFrameworkCore;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Infrastructure.Context;

namespace TaskPilot.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _context.ProjectTasks
                .Include(t => t.Project)
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Where(t => t.Project.Id == projectId)
                .Include(t => t.Comments)
                .ToListAsync();
        }

        public async Task<int> CountByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .CountAsync(t => t.Project.Id == projectId);
        }

        public async Task AddAsync(ProjectTask task)
        {
            await _context.ProjectTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTask task)
        {
            _context.ProjectTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProjectTask task)
        {
            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetPendingTasksByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Where(t => t.Project.Id == projectId && t.Status == ProjectTaskStatus.Pendente)
                .ToListAsync();
        }
    }
}
