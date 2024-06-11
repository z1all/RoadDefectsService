using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.DTOs.Common
{
    public class BasePagedDTO<TModels> where TModels : class
    {
        public virtual List<TModels> Models { protected get; set; } = null!;
        public PageInfoDTO Pagination { get; set; } = null!;
    }
}
