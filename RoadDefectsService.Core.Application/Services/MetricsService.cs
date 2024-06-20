using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly ICoordinateFixationDefectRepository _coordinateFixationDefectRepository;
        private readonly IFixationWorkRepository _fixationWorkRepository;
        private readonly IReportService _reportService;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MetricsService(
            IFixationWorkRepository fixationWorkRepository, IAssignmentRepository assignmentRepository, 
            ICoordinateFixationDefectRepository coordinateFixationDefectRepository, IReportService reportService, 
            IUserRepository userRepository, IMapper mapper)
        {
            _coordinateFixationDefectRepository = coordinateFixationDefectRepository;
            _fixationWorkRepository = fixationWorkRepository;
            _reportService = reportService;
            _assignmentRepository = assignmentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<List<CoordinateFixationDefectDTO>>> GetCoordinatesFixationsDefectsAsync(CoordinatesFilter filter)
        {
            List<CoordinateFixationDefect> coordinates = await _coordinateFixationDefectRepository.GetAllByFilterAsync(filter);

            return _mapper.Map<List<CoordinateFixationDefectDTO>>(coordinates);
        }

        public async Task<ExecutionResult<ReportDTO>> GetWorkReportAsync(Guid fixationWorkId, Guid userId)
        {
            FixationWork? fixationWork = await _fixationWorkRepository.GetByIdWithPhotosAndTaskWithPrevTaskAsync(fixationWorkId);
            if (fixationWork is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationWorkNotFound", $"Fixation defect with id {fixationWorkId} not found!");
            }

            Assignment? assignment = await _assignmentRepository.GetByFixationDefectIdWithAllNestingAsync(fixationWork.TaskFixationWork!.PrevTask!.FixationDefectId!.Value);
            if (assignment is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "AssignmentNotFound", $"Assignment defect with id {fixationWork.TaskFixationWork.FixationDefectId} not found!");
            }

            CustomUser? user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }
            
            GenerateWorkReportDTO generateWorkReport = new()
            {
                AssignmentId = assignment.Id,
                CreatedAssignmentDateTime = assignment.CreatedDateTime,
                Creator = _mapper.Map<UserInfoDTO>(user),
                Contractor = _mapper.Map<ContractorDTO>(assignment.Contractor),
                FixationDefect = _mapper.Map<FixationDefectDTO>(assignment.FixationDefect!),
                FixationWork = _mapper.Map<FixationWorkDTO>(fixationWork)
            };

            return _reportService.GenerateReport(generateWorkReport);
        }
    }
}
