using FluentAssertions;
using Moq;
using TaskPilot.Application.Dtos;
using TaskPilot.Application.UseCases.ProjectUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.ProjectUseCases
{
    public class UpdateProjectUseCaseTests
    {
        private readonly Mock<IProjectRepository> _projectRepoMock;
        private readonly UpdateProjectUseCase _useCase;

        public UpdateProjectUseCaseTests()
        {
            _projectRepoMock = new Mock<IProjectRepository>();
            _useCase = new UpdateProjectUseCase(_projectRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateProject_WhenProjectExists()
        {
            // Arrange
            var projectId = 1;
            var existingProject = new Project
            {
                Id = projectId,
                Name = "Projeto Antigo",
                Description = "Descrição antiga"
            };

            var updateDto = new UpdateProjectDto
            {
                Name = "Projeto Atualizado",
                Description = "Descrição atualizada"
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(existingProject);

            // Act
            var result = await _useCase.ExecuteAsync(projectId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(projectId);
            result.Name.Should().Be(updateDto.Name);
            result.Description.Should().Be(updateDto.Description);

            _projectRepoMock.Verify(r => r.GetByIdAsync(projectId), Times.Once);
            _projectRepoMock.Verify(r => r.UpdateAsync(It.Is<Project>(p =>
                p.Id == projectId &&
                p.Name == updateDto.Name &&
                p.Description == updateDto.Description
            )), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowKeyNotFoundException_WhenProjectNotFound()
        {
            // Arrange
            var projectId = 99;
            var updateDto = new UpdateProjectDto
            {
                Name = "Projeto Inexistente",
                Description = "Descrição inexistente"
            };

            _projectRepoMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync((Project?)null);

            // Act
            var act = async () => await _useCase.ExecuteAsync(projectId, updateDto);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Projeto não encontrado.");

            _projectRepoMock.Verify(r => r.GetByIdAsync(projectId), Times.Once);
            _projectRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
