using FluentAssertions;
using Moq;
using TaskPilot.Application.Dtos;
using TaskPilot.Application.UseCases.ProjectUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.ProjectUseCases
{
    public class CreateProjectUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldCreateProjectAndReturnDto()
        {
            // Arrange
            var dto = new CreateProjectDto
            {
                Name = "Projeto Teste",
                Description = "Descrição teste"
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Project>()))
                .Callback<Project>(proj => proj.Id = 1)
                .Returns(Task.CompletedTask);

            var useCase = new CreateProjectUseCase(projectRepositoryMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(dto);

            // Assert
            projectRepositoryMock.Verify(r => r.AddAsync(It.Is<Project>(p =>
                p.Name == dto.Name &&
                p.Description == dto.Description
            )), Times.Once);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be(dto.Name);
            result.Description.Should().Be(dto.Description);
        }
    }
}
