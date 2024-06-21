namespace RoadDefectsService.Core.Application.CQRS.DefectType.DTOs
{
    public class DefectTypeDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
