using Moq;
using TaskPilot.Application.UseCases.TaskUseCases;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Tests.Application.UseCases.TaskUseCases
{
    public class GetTasksByProjectUseCaseTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly GetTasksByProjectUseCase _useCase;

        public GetTasksByProjectUseCaseTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _useCase = new GetTasksByProjectUseCase(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_Returns_TaskDtos()
        {
            // Arrange
            int projectId = 123;
            var tasks = new List<ProjectTask>
        {
            new ProjectTask
            {
                Id = 1,
                Project = new Project { Id = projectId, Name = "Projeto Teste" },
                Title = "Tarefa 1",
                Description = "Descrição 1",
                DueDate = DateTime.Today.AddDays(7),
                Status = ProjectTaskStatus.Pendente,
                Priority = Priority.Media,
                Comments = new List<TaskComment>
                {
                    new TaskComment
                    {
                        Id = 10,
                        CommentText = "Comentário 1",
                        CreatedAt = DateTime.UtcNow,
                        UserId = "user1"
                    }
                }
            }
        };

            _taskRepositoryMock
                .Setup(repo => repo.GetByProjectIdAsync(projectId))
                .ReturnsAsync(tasks);

            // Act
            var result = await _useCase.ExecuteAsync(projectId);

            // Assert
            Assert.Single(result);
            var taskDto = Assert.Single(result);
            Assert.Equal(1, taskDto.Id);
            Assert.Equal(projectId.ToString(), taskDto.ProjectId);
            Assert.Equal("Tarefa 1", taskDto.Title);
            Assert.Single(taskDto.Comments);
            Assert.Equal("Comentário 1", taskDto.Comments[0].Content);
        }
    }
}
