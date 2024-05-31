using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/contractor")]
    [ApiController]
    [Authorize(Roles = Role.Operator)]
    public class ContractorController : ControllerBase
    {
        /// <summary>
        /// Все подрядчики (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("contractors")]
        public async Task<ActionResult> GetСontractors()
        {
            return Ok();
        }

        /// <summary>
        /// Конкретный подрядчик (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("{contractorId}")]
        public async Task<ActionResult> GetСontractor([FromRoute] Guid contractorId)
        {
            return Ok();
        }

        /// <summary>
        /// Создать подрядчика (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost]
        public async Task<ActionResult> CreateСontractor()
        {
            return Ok();
        }

        /// <summary>
        /// Редактировать информацию подрядчика (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("{contractorId}")]
        public async Task<ActionResult> ChangeСontractor([FromRoute] Guid contractorId)
        {
            return Ok();
        }
    }
}
