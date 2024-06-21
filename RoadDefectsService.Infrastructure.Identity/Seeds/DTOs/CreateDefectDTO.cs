namespace RoadDefectsService.Infrastructure.Identity.Seeds.DTOs
{
    public class CreateDefectDTO
    {
        public required Guid Id { get; set; }
        public required Guid TaskId { get; set; }
        public required double DamagedCanvasSquareMeter { get; set; }
        public required Guid DefectTypeId { get; set; }
    }
}
