namespace RoadDefectsService.Core.Application.DTOs.PhotoService
{
    public class PhotoDTO
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required byte[] File { get; set; }
    }
}
