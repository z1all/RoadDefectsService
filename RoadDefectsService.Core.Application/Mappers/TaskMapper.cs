using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class TaskMapper
    {
        public static TaskDTO ToTaskDTO(this TaskEntity task)
        {
            return new()
            {
                Id = task.Id,
                TaskType = task.TaskType,
                TaskStatus = task.TaskStatus,
                CreatedDateTime = task.CreatedDateTime,
                DefectStatus = task.DefectStatus,
                ExistRoadInspector = task.RoadInspectorId is not null,
                ExistDefectInfo = false,
            };
        }

        public static List<TaskDTO> ToTaskDTOList(this List<TaskEntity> tasks)
        {
            return tasks.Select(ToTaskDTO).ToList();
        }

        public static FixationWorkTaskDTO ToFixationWorkTaskDTO(this TaskFixationWork task)
        {
            return new()
            {
                Id = task.Id,
                TaskStatus = task.TaskStatus,
                CreatedDateTime = task.CreatedDateTime,
                DefectStatus = task.DefectStatus,
                DefectFixation = task.FixationDefect?.ToFixationDefectDTO() ?? null,
                FixationWork = task.FixationWork?.ToFixationWorkDTO() ?? null,
                Executor = task.RoadInspector?.ToRoadInspectorDTO(),
                PrevTask = task.PrevTask!.ToTaskDTO(),
            };
        }

        public static FixationDefectTaskDTO ToFixationDefectTaskDTO(this TaskFixationDefect task)
        {
            return new()
            {
                Id = task.Id,
                TaskStatus = task.TaskStatus,
                CreatedDateTime = task.CreatedDateTime,
                DefectStatus = task.DefectStatus,
                ApproximateAddress = task.ApproximateAddress,
                Description = task.Description,
                DefectFixation = task.FixationDefect?.ToFixationDefectDTO() ?? null,
                Executor = task.RoadInspector?.ToRoadInspectorDTO(),
            };
        }
    }
}
