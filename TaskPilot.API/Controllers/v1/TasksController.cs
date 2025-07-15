using Microsoft.AspNetCore.Mvc;
using TaskPilot.Application.Dtos;
using TaskPilot.Application.UseCases.TaskUseCases;

namespace TaskPilot.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/projects/{projectId}/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly GetTasksByProjectUseCase _getTasksUseCase;
        private readonly CreateTaskUseCase _createTaskUseCase;
        private readonly UpdateTaskUseCase _updateTaskUseCase;
        private readonly DeleteTaskUseCase _deleteTaskUseCase;

        public TasksController(
            GetTasksByProjectUseCase getTasksUseCase,
            CreateTaskUseCase createTaskUseCase,
            UpdateTaskUseCase updateTaskUseCase,
            DeleteTaskUseCase deleteTaskUseCase)
        {
            _getTasksUseCase = getTasksUseCase;
            _createTaskUseCase = createTaskUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
        }

        /// <summary>
        /// Lista todas as tarefas de um projeto.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTasks(int projectId)
        {
            var tasks = await _getTasksUseCase.ExecuteAsync(projectId);
            return Ok(tasks);
        }

        /// <summary>
        /// Cria uma nova tarefa em um projeto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] CreateTaskDto dto)
        {
            var userId = new Random().Next(1, 100).ToString();
            var newTask = await _createTaskUseCase.ExecuteAsync(projectId, dto, userId);
            return CreatedAtAction(nameof(GetTasks), new { projectId }, newTask);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int projectId, int taskId, [FromBody] UpdateTaskDto dto)
        {
            var userId = new Random().Next(1, 100);
            await _updateTaskUseCase.ExecuteAsync(projectId, taskId, dto, userId.ToString());
            return NoContent();
        }

        /// <summary>
        /// Remove uma tarefa de um projeto.
        /// </summary>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int projectId, int taskId)
        {
            var userId = new Random().Next(1, 100);
            await _deleteTaskUseCase.ExecuteAsync(taskId, userId: userId);
            return NoContent();
        }
    }
}
