using FluentAssertions;
using Moq;
using TaskPilot.Application.UseCases.TaskUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.TaskUseCases
{
    public class DeleteTaskUseCaseTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly Mock<ITaskHistoryRepository> _historyRepoMock;
        private readonly DeleteTaskUseCase _useCase;

        public DeleteTaskUseCaseTests()
        {
            _taskRepoMock = new Mock<ITaskRepository>();
            _historyRepoMock = new Mock<ITaskHistoryRepository>();

            _useCase = new DeleteTaskUseCase(_taskRepoMock.Object, _historyRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteTaskAndLogHistory_WhenTaskExists()
        {
            // Arrange
            var project = new Project { Id = 1, Name = "Projeto A" };
            var task = new ProjectTask
            {
                Id = 10,
                Title = "Tarefa Teste",
                Project = project
            };

            _taskRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(task);

            // Act
            await _useCase.ExecuteAsync(10, 99);

            // Assert
            _historyRepoMock.Verify(r => r.AddAsync(It.Is<TaskHistory>(h =>
                h.ProjectTaskId == project.Id &&
                h.ChangedByUserId == "99" &&
                h.ChangeDescription == "Tarefa removida" &&
                h.ProjectTask == task
            )), Times.Once);

            _taskRepoMock.Verify(r => r.DeleteAsync(task), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenTaskNotFound()
        {
            // Arrange
            _taskRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync((ProjectTask?)null);

            // Act
            var act = async () => await _useCase.ExecuteAsync(10, 99);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Tarefa não encontrada.");

            _historyRepoMock.Verify(r => r.AddAsync(It.IsAny<TaskHistory>()), Times.Never);
            _taskRepoMock.Verify(r => r.DeleteAsync(It.IsAny<ProjectTask>()), Times.Never);
        }
    }
}
