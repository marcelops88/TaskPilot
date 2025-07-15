using FluentAssertions;
using Moq;
using TaskPilot.Application.UseCases.ProjectUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.ProjectUseCases
{
    public class GetProjectByIdUseCaseTests
    {
        private readonly Mock<IProjectRepository> _projectRepoMock;
        private readonly GetProjectByIdUseCase _useCase;

        public GetProjectByIdUseCaseTests()
        {
            _projectRepoMock = new Mock<IProjectRepository>();
            _useCase = new GetProjectByIdUseCase(_projectRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnProject_WhenProjectExists()
        {
            // Arrange
            var projectId = 1;
            var project = new Project
            {
                Id = projectId,
                Name = "Projeto Teste",
                Description = "Descrição Teste"
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act
            var result = await _useCase.ExecuteAsync(projectId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(projectId);
            result.Name.Should().Be("Projeto Teste");
            result.Description.Should().Be("Descrição Teste");

            _projectRepoMock.Verify(r => r.GetByIdAsync(projectId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowKeyNotFoundException_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectId = 99;
            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync((Project?)null);

            // Act
            var act = async () => await _useCase.ExecuteAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Projeto não encontrado.");

            _projectRepoMock.Verify(r => r.GetByIdAsync(projectId), Times.Once);
        }
    }
}
