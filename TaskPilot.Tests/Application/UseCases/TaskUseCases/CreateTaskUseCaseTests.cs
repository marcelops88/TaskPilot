using FluentAssertions;
using Moq;
using TaskPilot.Application.Dtos;
using TaskPilot.Application.UseCases.TaskUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.TaskUseCases
{
    public class CreateTaskUseCaseTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly Mock<IProjectRepository> _projectRepoMock;
        private readonly Mock<ITaskHistoryRepository> _historyRepoMock;
        private readonly CreateTaskUseCase _useCase;

        public CreateTaskUseCaseTests()
        {
            _taskRepoMock = new Mock<ITaskRepository>();
            _projectRepoMock = new Mock<IProjectRepository>();
            _historyRepoMock = new Mock<ITaskHistoryRepository>();

            _useCase = new CreateTaskUseCase(
                _taskRepoMock.Object,
                _projectRepoMock.Object,
                _historyRepoMock.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCreateTask_WhenDataIsValid()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Test Project",
                ProjectTask = new List<ProjectTask>()
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(project);

            var dto = new CreateTaskDto
            {
                Title = "New Task",
                Description = "Test Description",
                DueDate = DateTime.UtcNow.AddDays(5),
                Priority = Priority.Media
            };

            // Act
            var result = await _useCase.ExecuteAsync(1, dto, "user-1");

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(dto.Title);
            result.Description.Should().Be(dto.Description);
            result.DueDate.Should().Be(dto.DueDate);
            result.Status.Should().Be(ProjectTaskStatus.Pendente);
            result.Priority.Should().Be(dto.Priority);

            _taskRepoMock.Verify(r => r.AddAsync(It.IsAny<ProjectTask>()), Times.Once);
            _historyRepoMock.Verify(r => r.AddAsync(It.IsAny<TaskHistory>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenProjectNotFound()
        {
            // Arrange
            _projectRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Project?)null);

            var dto = new CreateTaskDto
            {
                Title = "New Task",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = Priority.Alta
            };

            // Act
            var act = async () => await _useCase.ExecuteAsync(1, dto, "user-1");

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Projeto não encontrado.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenTaskLimitExceeded()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Test Project",
                ProjectTask = new List<ProjectTask>(new ProjectTask[20])
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(project);

            var dto = new CreateTaskDto
            {
                Title = "New Task",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = Priority.Baixa
            };

            // Act
            var act = async () => await _useCase.ExecuteAsync(1, dto, "user-1");

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Limite máximo de 20 tarefas por projeto atingido.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenDueDateIsNull()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Test Project",
                ProjectTask = new List<ProjectTask>()
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(project);

            var dto = new CreateTaskDto
            {
                Title = "New Task",
                Priority = Priority.Media
            };

            // Act
            var act = async () => await _useCase.ExecuteAsync(1, dto, "user-1");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("A data de vencimento é obrigatória.*");
        }
    }
}
