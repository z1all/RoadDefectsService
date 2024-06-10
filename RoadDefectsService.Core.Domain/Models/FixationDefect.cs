﻿namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationDefect : Fixation
    {
        public required DateTime RecordedDateTime { get; set; }
        public required string ExactAddress { get; set; }
        public required double CoordinatesX { get; set; }
        public required double CoordinatesY { get; set; }
        public required double DamagedCanvasSquareMeter { get; set; }

        public required Guid DefectTypeId { get; set; }
        public DefectType? DefectType { get; set; }

        public TaskEntity? Task { get; set; }
    }
}
