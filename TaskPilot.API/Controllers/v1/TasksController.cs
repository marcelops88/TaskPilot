using Microsoft.AspNetCore.Mvc;
using TaskPilot.Application.Dtos;
using TaskPilot.Domain.Enums;

namespace TaskPilot.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/projects/{projectId}/[controller]")]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// Lista todas as tarefas de um projeto.
        /// </summary>
        [HttpGet]
        public IActionResult GetTasks(int projectId)
        {
            var tasks = new List<TaskDto>
            {
                new TaskDto { Id = 1, Title = "Tarefa exemplo", Description = "Detalhes", DueDate = DateTime.Today.AddDays(7), Status = ProjectTaskStatus.Pendente }
            };

            return Ok(tasks);
        }

        /// <summary>
        /// Cria uma nova tarefa em um projeto.
        /// </summary>
        [HttpPost]
        public IActionResult CreateTask(int projectId, [FromBody] CreateTaskDto dto)
        {
            var newTask = new TaskDto
            {
                Id = 1,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = ProjectTaskStatus.Pendente
            };

            return CreatedAtAction(nameof(GetTasks), new { projectId = projectId }, newTask);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int projectId, int taskId, [FromBody] UpdateTaskDto dto)
        {

            return NoContent();
        }

        /// <summary>
        /// Remove uma tarefa de um projeto.
        /// </summary>
        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int projectId, int taskId)
        {

            return NoContent();
        }
    }
}
