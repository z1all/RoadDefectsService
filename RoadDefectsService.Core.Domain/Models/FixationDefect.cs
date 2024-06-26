﻿namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationDefect : Fixation
    {
        public required DateTime RecordedDateTime { get; set; }
        public double? DamagedCanvasSquareMeter { get; set; }
        public bool IsEliminated { get; set; } = false;

        public required string CacheAddress { get; set; }

        public Guid? DefectTypeId { get; set; }
        public DefectType? DefectType { get; set; }

        public TaskEntity? Task { get; set; }
    }
}
