using Microsoft.AspNetCore.Mvc;
using TaskPilot.Application.UseCases.ReportsUseCases;

namespace TaskPilot.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly GetPerformanceReportUseCase _reportUseCase;

        public ReportsController(GetPerformanceReportUseCase reportUseCase)
        {
            _reportUseCase = reportUseCase;
        }

        /// <summary>
        /// Obtém relatório de desempenho (apenas para gerentes).
        /// </summary>
        [HttpGet("performance")]
        public async Task<IActionResult> GetPerformanceReport()
        {
            var isManager = User.Identity?.IsAuthenticated == true && User.IsInRole("Manager");

            if (!isManager)
                return StatusCode(403, new { message = "Apenas usuários com a função de gerente podem acessar este relatório." });

            var result = await _reportUseCase.ExecuteAsync();
            return Ok(result);
        }
    }
}
