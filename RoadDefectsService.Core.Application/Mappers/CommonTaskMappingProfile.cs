using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class CommonTaskMappingProfile : Profile
    {
        public CommonTaskMappingProfile()
        {
            CreateMap<TaskFixationDefectEntity, FixationDefectTaskDTO>()
                .ForMember(
                    fixationDefectTaskDTO => fixationDefectTaskDTO.DefectFixation,
                    options => options.MapFrom(taskFixationDefect => taskFixationDefect.FixationDefect)
                )
                .ForMember(
                    fixationDefectTaskDTO => fixationDefectTaskDTO.Executor,
                    options => options.MapFrom(taskFixationDefect => taskFixationDefect.RoadInspector!.User)
                );

            CreateMap<TaskFixationWorkEntity, FixationWorkTaskDTO>()
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
