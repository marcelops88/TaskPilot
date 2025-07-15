using FluentAssertions;
using Moq;
using TaskPilot.Application.UseCases.ProjectUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.ProjectUseCases
{
    public class GetAllProjectsUseCaseTests
    {
        private readonly Mock<IProjectRepository> _projectRepoMock;
        private readonly GetAllProjectsUseCase _useCase;

        public GetAllProjectsUseCaseTests()
        {
            _projectRepoMock = new Mock<IProjectRepository>();
            _useCase = new GetAllProjectsUseCase(_projectRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnMappedProjectDtos()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { Id = 1, Name = "Projeto A", Description = "Desc A" },
                new Project { Id = 2, Name = "Projeto B", Description = "Desc B" }
            };

            _projectRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var projectList = result.ToList();
            projectList[0].Id.Should().Be(1);
            projectList[0].Name.Should().Be("Projeto A");
            projectList[0].Description.Should().Be("Desc A");

            projectList[1].Id.Should().Be(2);
            projectList[1].Name.Should().Be("Projeto B");
            projectList[1].Description.Should().Be("Desc B");

            _projectRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenNoProjectsExist()
        {
            // Arrange
            _projectRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Project>());

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _projectRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
