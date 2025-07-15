using FluentAssertions;
using Moq;
using TaskPilot.Application.UseCases.ReportsUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.ReportsUseCases
{
    public class GetPerformanceReportUseCaseTests
    {
        private readonly Mock<ITaskHistoryRepository> _historyRepoMock;
        private readonly GetPerformanceReportUseCase _useCase;

        public GetPerformanceReportUseCaseTests()
        {
            _historyRepoMock = new Mock<ITaskHistoryRepository>();
            _useCase = new GetPerformanceReportUseCase(_historyRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnUserPerformanceDtos()
        {
            // Arrange
            var mockData = new List<UserPerformanceData>
            {
                new UserPerformanceData { UserId = 1, CompletedTasks = 5 },
                new UserPerformanceData { UserId = 2, CompletedTasks = 8 }
            };

            _historyRepoMock
                .Setup(r => r.GetCompletedTasksCountPerUserAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(mockData);

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var list = result.ToList();
            list[0].UserId.Should().Be(1);
            list[0].CompletedTasks.Should().Be(5);
            list[1].UserId.Should().Be(2);
            list[1].CompletedTasks.Should().Be(8);

            _historyRepoMock.Verify(r => r.GetCompletedTasksCountPerUserAsync(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoData()
        {
            // Arrange
            _historyRepoMock
                .Setup(r => r.GetCompletedTasksCountPerUserAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(new List<UserPerformanceData>());

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _historyRepoMock.Verify(r => r.GetCompletedTasksCountPerUserAsync(It.IsAny<DateTime>()), Times.Once);
        }
    }
}
