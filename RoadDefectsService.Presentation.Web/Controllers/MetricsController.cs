using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Controllers.Base;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    [Authorize(Roles = Role.Operator)]
    public class MetricsController : BaseController
    {
        /// <summary>
        /// Получить отчет о проведенных работах по устранению дефекта (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("report")]
        public async Task<ActionResult> GetReport()
        {
            return Ok();
        }

        /// <summary>
        /// Собрать статистику по проведенным работам (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("statistic")]
        public async Task<ActionResult> GetStatistic()
        {
            return Ok();
        }
    }
}
