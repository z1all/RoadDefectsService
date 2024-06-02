using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.Other
{
    public class ModalStateOptionsConfigure : IConfigureOptions<ApiBehaviorOptions>
    {
        public void Configure(ApiBehaviorOptions options)
        {
            // Отключение автоматической проверки ModelState.IsValid
            options.SuppressModelStateInvalidFilter = true;
        }
    }
}
