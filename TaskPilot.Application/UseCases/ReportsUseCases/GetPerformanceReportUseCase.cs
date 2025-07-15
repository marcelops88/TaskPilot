using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.UseCases.ReportsUseCases
{
    public class GetPerformanceReportUseCase
    {
        private readonly ITaskHistoryRepository _taskHistoryRepository;

        public GetPerformanceReportUseCase(ITaskHistoryRepository taskHistoryRepository)
        {
            _taskHistoryRepository = taskHistoryRepository;
        }
        public async Task<IEnumerable<UserPerformanceDto>> ExecuteAsync()
        {
            var fromDate = DateTime.UtcNow.AddDays(-30);
            var data = await _taskHistoryRepository.GetCompletedTasksCountPerUserAsync(fromDate);

            return data.Select(d => new UserPerformanceDto
            {
                UserId = d.UserId,
                CompletedTasks = d.CompletedTasks
            });
        }

    }
}
