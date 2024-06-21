using AutoMapper;
using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Utils
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskEntity, TaskDTO>()
               .ForMember(
                   taskDTO => taskDTO.ExistRoadInspector,
                   options => options.MapFrom(taskEntity => taskEntity.RoadInspectorId != null)
               )
               .ForMember(
                   taskDTO => taskDTO.ExistDefectInfo,
                   options => options.MapFrom(taskEntity => taskEntity.FixationDefectId != null)
               );
        }
    }
}
