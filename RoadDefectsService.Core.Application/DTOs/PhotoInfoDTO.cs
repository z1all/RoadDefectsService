namespace RoadDefectsService.Core.Application.DTOs
{
    public class PhotoInfoDTO
    {
        public required Guid Id { get; set; }
        public required string Type { get; set; }
        public required string Name { get; set; }
    }
}
