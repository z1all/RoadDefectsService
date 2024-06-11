using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    [CustomeAuthorize(Roles = Role.Operator)]
    [SwaggerControllerOrder(Order = 11)]
    public class MetricsController : BaseController
    {
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
        /// Посмотреть зафиксированные дефекты на карте за определенный период(Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("fixations_defects_coordinates")]
        public async Task<ActionResult> GetCoordinatesFixationsDefects()
        {
            return Ok();
        }
    }
}
