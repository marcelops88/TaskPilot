using Moq;
using TaskPilot.Application.Dtos;
using TaskPilot.Application.UseCases.TaskUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.TaskUseCases
{
    public class UpdateTaskUseCaseTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly Mock<ITaskHistoryRepository> _historyRepoMock;
        private readonly UpdateTaskUseCase _useCase;

        public UpdateTaskUseCaseTests()
        {
            _taskRepoMock = new Mock<ITaskRepository>();
            _historyRepoMock = new Mock<ITaskHistoryRepository>();
            _useCase = new UpdateTaskUseCase(_taskRepoMock.Object, _historyRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_UpdatesTaskAndAddsHistory()
        {
            // Arrange
            var projectId = 1;
            var taskId = 10;
            var userId = "user123";

            var existingTask = new ProjectTask
            {
                Id = taskId,
                Project = new Project { Id = projectId },
                Title = "Old title",
                Description = "Old description",
                DueDate = DateTime.Today,
                Status = ProjectTaskStatus.Pendente,
                Priority = Priority.Alta
            };

            var dto = new UpdateTaskDto
            {
                Title = "New title",
                Description = "New description",
                DueDate = DateTime.Today.AddDays(5),
                Status = ProjectTaskStatus.Concluida
            };

            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
            _taskRepoMock.Setup(r => r.UpdateAsync(existingTask)).Returns(Task.CompletedTask);
            _historyRepoMock.Setup(r => r.AddAsync(It.IsAny<TaskHistory>())).Returns(Task.CompletedTask);

            // Act
            await _useCase.ExecuteAsync(projectId, taskId, dto, userId);

            // Assert
            Assert.Equal(dto.Title, existingTask.Title);
            Assert.Equal(dto.Description, existingTask.Description);
            Assert.Equal(dto.DueDate.Value, existingTask.DueDate);
            Assert.Equal(dto.Status.Value, existingTask.Status);

            _taskRepoMock.Verify(r => r.UpdateAsync(existingTask), Times.Once);
            _historyRepoMock.Verify(r => r.AddAsync(It.Is<TaskHistory>(h =>
                h.ProjectTaskId == projectId &&
                h.ChangedByUserId == userId &&
                h.ChangeDescription == "Tarefa atualizada")), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ThrowsKeyNotFoundException_WhenTaskNotFound()
        {
            // Arrange
            _taskRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ProjectTask?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _useCase.ExecuteAsync(1, 1, new UpdateTaskDto(), "user"));
        }

        [Fact]
        public async Task ExecuteAsync_ThrowsKeyNotFoundException_WhenProjectIdDoesNotMatch()
        {
            // Arrange
            var task = new ProjectTask { Id = 1, Project = new Project { Id = 999 } };
            _taskRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(task);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _useCase.ExecuteAsync(1, 1, new UpdateTaskDto(), "user"));
        }
    }
}
