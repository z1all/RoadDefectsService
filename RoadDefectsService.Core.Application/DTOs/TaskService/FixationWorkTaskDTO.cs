﻿using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class FixationWorkTaskDTO
    {
        public required Guid Id { get; set; }
        public required bool IsTransfer { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required DefectStatus DefectStatus { get; set; }
        public required StatusTask TaskStatus { get; set; }
        public required FixationDefectDTO? DefectFixation { get; set; }
        public required FixationWorkDTO? FixationWork { get; set; }
        public required TaskDTO PrevTask { get; set; }
        public required RoadInspectorDTO? Executor { get; set; }
    }
}
