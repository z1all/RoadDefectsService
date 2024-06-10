using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api/defect_type")]
    [ApiController]
    public class DefectTypeController : BaseController
    {
        private readonly IDefectTypeService _defectTypeService;

        public DefectTypeController(IDefectTypeService defectTypeService)
        {
            _defectTypeService = defectTypeService;
        }

        /// <summary>
        /// Типы дефектов
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(List<DefectTypeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DefectTypeDTO>>> GetDefectTypes([FromQuery] DefectTypeFilterDTO defectTypeFilter)
        {
            return await ExecutionResultHandlerAsync(() => _defectTypeService.GetDefectTypesAsync(defectTypeFilter));
        }
    }
}
