using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskEntity, TaskDTO>()
                    .ForMember(
                       taskDTO => taskDTO.Address,
                       options => options.MapFrom(taskEntity => taskEntity.ApproximateAddress)
                   )
                   .ForMember(
                       taskDTO => taskDTO.ExistRoadInspector,
                       options => options.MapFrom(taskEntity => taskEntity.RoadInspectorId != null)
                   )
                   .ForMember(
                       taskDTO => taskDTO.ExistDefectInfo,
                       options => options.MapFrom(taskEntity => taskEntity.FixationDefectId != null)
                   );

            CreateMap<TaskFixationDefect, FixationDefectTaskDTO>()
                .ForMember(
                    fixationDefectTaskDTO => fixationDefectTaskDTO.DefectFixation,
                    options => options.MapFrom(taskFixationDefect => taskFixationDefect.FixationDefect)
                )
                .ForMember(
                    fixationDefectTaskDTO => fixationDefectTaskDTO.Executor,
                    options => options.MapFrom(taskFixationDefect => taskFixationDefect.RoadInspector!.User)
                );

            CreateMap<TaskFixationWork, FixationWorkTaskDTO>()
                .ForMember(
                    fixationDefectTaskDTO => fixationDefectTaskDTO.DefectFixation,
                    options => options.MapFrom(taskFixationDefect => taskFixationDefect.FixationDefect)
                )
                .ForMember(
                    fixationWorkTaskDTO => fixationWorkTaskDTO.Executor,
                    options => options.MapFrom(taskFixationWork => taskFixationWork.RoadInspector!.User)
                );
        }
    }
}
