using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Application.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/metrics")]
    [ApiController]
    [CustomeAuthorize(Roles = Role.Operator)]
    [SwaggerControllerOrder(Order = 11)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class MetricsController : BaseController
    {
        private readonly IMetricsService _metricsService;

        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        /// <summary>
        /// Получить отчет о проведенных работах по устранению дефекта (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("fixation_work/{fixationWorkId}/report")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetReport([FromRoute] Guid fixationWorkId)
        {
            if (!HttpContext.TryGetUserId(out Guid userId))
            {
                return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "UnknowError", "Unknow error"));
            }

            ExecutionResult<ReportDTO> response = await _metricsService.GetWorkReportAsync(fixationWorkId, userId);
            if (!response.TryGetResult(out ReportDTO report))
            {
                return ExecutionResultHandler(response);
            }

            return File(report.File, "application/pdf", report.Name);
        }

        /// <summary>
        /// Собрать статистику по проведенным работам (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("statistic")]
        public async Task<ActionResult> GetStatistic()
        {
            return Ok();
        }

        /// <summary>
        /// Посмотреть зафиксированные дефекты на карте за определенный период
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("fixations_defects_coordinates")]
        [ProducesResponseType(typeof(List<CoordinateFixationDefectDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CoordinateFixationDefectDTO>>> GetCoordinatesFixationsDefects([FromQuery] CoordinatesFilter coordinatesFilter)
        {
            return await ExecutionResultHandlerAsync(() => _metricsService.GetCoordinatesFixationsDefectsAsync(coordinatesFilter));
        }
    }
}
