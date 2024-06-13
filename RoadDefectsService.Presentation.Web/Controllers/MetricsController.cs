using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

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
        /// Получить отчет о проведенных работах по устранению дефекта (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("report")]
        public async Task<ActionResult> GetReport()
        {
            return Ok();
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
        /// Посмотреть зафиксированные дефекты на карте за определенный период (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("fixations_defects_coordinates")]
        [ProducesResponseType(typeof(List<CoordinateInfo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CoordinateInfo>>> GetCoordinatesFixationsDefects([FromQuery] CoordinatesFilter coordinatesFilter)
        {
            return await ExecutionResultHandlerAsync(() => _metricsService.GetCoordinatesFixationsDefectsAsync(coordinatesFilter));
        }
    }
}
