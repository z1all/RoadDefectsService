namespace RoadDefectsService.Core.Domain
{
    public class CoordinateFixationDefect
    {
        public required Guid FixationDefectId { get; set; }
        public required DateTime FixationDateTime { get; set; }
        public required bool IsEliminated { get; set; }
        public required double? X { get; set; }
        public required double? Y { get; set; }
    }
}
