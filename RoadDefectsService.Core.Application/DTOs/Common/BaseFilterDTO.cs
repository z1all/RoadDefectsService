namespace RoadDefectsService.Core.Application.DTOs.Common
{
    public class BaseFilterDTO
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
