namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class PhotoInfoDTO
    {
        public required Guid Id { get; set; }
        public required string Type { get; set; }
        public required string Name { get; set; }
        public string PathName { get => $"{Id}_{Name}{Type}"; }
    }
}
