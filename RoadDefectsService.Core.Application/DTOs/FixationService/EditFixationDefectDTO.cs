namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class EditFixationDefectDTO
    {
        public required double DamagedCanvasSquareMeter { get; set; }
        public required Guid DefectTypeId { get; set; }
    }
}
