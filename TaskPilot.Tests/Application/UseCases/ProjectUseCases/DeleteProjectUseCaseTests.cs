using FluentAssertions;
using Moq;
using TaskPilot.Application.UseCases.ProjectUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.ProjectUseCases
{
    public class DeleteProjectUseCaseTests
    {
        private readonly Mock<IProjectRepository> _projectRepoMock;
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly DeleteProjectUseCase _useCase;

        public DeleteProjectUseCaseTests()
        {
            _projectRepoMock = new Mock<IProjectRepository>();
            _taskRepoMock = new Mock<ITaskRepository>();
            _useCase = new DeleteProjectUseCase(_projectRepoMock.Object, _taskRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteProject_WhenNoPendingTasks()
        {
            // Arrange
            var projectId = 1;
            var project = new Project { Id = projectId, Name = "Projeto Teste" };

            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(project);
            _taskRepoMock.Setup(r => r.GetPendingTasksByProjectIdAsync(projectId)).ReturnsAsync(new List<ProjectTask>());

            // Act
            await _useCase.ExecuteAsync(projectId);

            // Assert
            _projectRepoMock.Verify(r => r.DeleteAsync(project), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenProjectHasPendingTasks()
        {
            // Arrange
            var projectId = 1;
            var project = new Project { Id = projectId, Name = "Projeto Teste" };
            var pendingTasks = new List<ProjectTask>
            {
                new ProjectTask { Id = 10, Title = "Task pendente" }
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(project);
            _taskRepoMock.Setup(r => r.GetPendingTasksByProjectIdAsync(projectId)).ReturnsAsync(pendingTasks);

            // Act
            var act = async () => await _useCase.ExecuteAsync(projectId);

            // Assert
            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Não é possível remover o projeto. Existem tarefas pendentes associadas. Conclua ou remova as tarefas primeiro.");

            _projectRepoMock.Verify(r => r.DeleteAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowKeyNotFound_WhenProjectNotFound()
        {
            // Arrange
            var projectId = 1;
            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync((Project?)null);

            // Act
            var act = async () => await _useCase.ExecuteAsync(projectId);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("Projeto não encontrado.");

            _projectRepoMock.Verify(r => r.DeleteAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
