using AutoMapper;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly INotificationService _notificationService;
        private readonly IFixationDefectRepository _fixationDefectRepository;
        private readonly IContractorRepository _contractorRepository;
        private readonly IMapper _mapper;

        public AssignmentService(
            IAssignmentRepository assignmentRepository, INotificationService notificationService,
            IFixationDefectRepository fixationDefectRepository, IContractorRepository contractorRepository,
            IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _notificationService = notificationService;
            _fixationDefectRepository = fixationDefectRepository;
            _contractorRepository = contractorRepository;
            _mapper = mapper;
        }

        public Task<ExecutionResult<AssignmentPagedDTO>> GetAssignmentsAsync(AssignmentFilterDTO assignmentFilterDTO)
        {
            return FiltrationHelper
                .FilterAsync<AssignmentFilterDTO, AssignmentEntity, AssignmentShortInfoDTO, AssignmentPagedDTO>(
                assignmentFilterDTO, _assignmentRepository, (assignments) => _mapper.Map<List<AssignmentShortInfoDTO>>(assignments));
        }

        public async Task<ExecutionResult<AssignmentDTO>> GetAssignmentAsync(Guid assignmentId)
        {
            AssignmentEntity? assignment = await _assignmentRepository.GetByIdWithContractorAndFixationDefectWithDefectTypeAndPhotosAsync(assignmentId);
            if (assignment is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "AssignmentNotFound", $"Assignment with id {assignmentId} not found!");
            }

            return _mapper.Map<AssignmentDTO>(assignment);
        }

        public async Task<ExecutionResult> CreateAssignmentAsync(CreateAssignmentDTO createAssignment, Guid? userId)
        {
            FixationDefectEntity? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAndPhotosAndDefectTypeAsync(createAssignment.FixationDefectId);
            if (fixationDefect is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationDefectNotFound", $"Fixation defect with id {createAssignment.FixationDefectId} not found!");
            }

            if (userId is null && !fixationDefect.Task!.IsTransfer)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskIsNotTransfer", $"The task should be to transfer data from paper to electronic form!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndCompletedTaskStatus(fixationDefect.Task!, userId);
            if (checkResult.IsNotSuccess) return checkResult;

            bool existAssignmentWithSameFixationDefect = await _assignmentRepository.AnyByFixationDefectIdAsync(createAssignment.FixationDefectId);
            if (existAssignmentWithSameFixationDefect)
            {
                return new(StatusCodeExecutionResult.BadRequest, "AssignmentAlreadyExist", $"Fixation defect with id {createAssignment.FixationDefectId} already has a request for work!");
            }

            ContractorEntity? contractor = await _contractorRepository.GetByIdAsync(createAssignment.ContractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor defect with id {createAssignment.ContractorId} not found!");
            }

            AssignmentEntity assignment = new()
            {
                CreatedDateTime = DateTime.UtcNow,
                DeadlineDateOnly = createAssignment.DeadlineDateOnly,
                FixationDefectId = createAssignment.FixationDefectId,
                ContractorId = createAssignment.ContractorId,
            };
            await _assignmentRepository.AddAsync(assignment);

            if (userId is not null)
            {
                ExecutionResult sendResult = await SendCreatedAssignmentNotificationAsync(assignment, contractor, fixationDefect);
                if (sendResult.IsNotSuccess)
                {
                    await _assignmentRepository.DeleteAsync(assignment);
                    return sendResult;
                }
            }

            return ExecutionResult.SucceededResult;
        }

        private async Task<ExecutionResult> SendCreatedAssignmentNotificationAsync(AssignmentEntity assignment, ContractorEntity contractor, FixationDefectEntity fixationDefect)
        {
            try
            {
                CreatedAssignmentNotificationDTO notification = new()
                {
                    AssignmentId = assignment.Id,
                    DeadlineDateOnly = assignment.DeadlineDateOnly,
                    Contractor = _mapper.Map<ContractorDTO>(contractor),
                    FixationDefect = _mapper.Map<FixationDefectWithPhotoShortInfoDTO>(fixationDefect),
                };

                ExecutionResult sendResult = await _notificationService.SendCreatedAssignmentNotificationAsync(notification);
                if (sendResult.IsNotSuccess) return sendResult;

                return ExecutionResult.SucceededResult;
            }
            catch
            {
                return new(StatusCodeExecutionResult.InternalServer, "UnknownError", "Unknown error during message sending!");
            }
        }
    }
}
