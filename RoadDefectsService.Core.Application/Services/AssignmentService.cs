using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;
using System.Diagnostics.Contracts;

namespace RoadDefectsService.Core.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly INotificationService _notificationService;
        private readonly IFixationDefectRepository _fixationDefectRepository;
        private readonly IContractorRepository _contractorRepository;

        public AssignmentService(
            IAssignmentRepository assignmentRepository, INotificationService notificationService,
            IFixationDefectRepository fixationDefectRepository, IContractorRepository contractorRepository)
        {
            _assignmentRepository = assignmentRepository;
            _notificationService = notificationService;
            _fixationDefectRepository = fixationDefectRepository;
            _contractorRepository = contractorRepository;
        }

        public Task<ExecutionResult<AssignmentPagedDTO>> GetAssignmentsAsync(AssignmentFilterDTO assignmentFilterDTO)
        {
            return FiltrationHelper
                .FilterAsync<AssignmentFilterDTO, Assignment, AssignmentShortInfoDTO, AssignmentPagedDTO>(
                assignmentFilterDTO, _assignmentRepository, (assignments) => assignments.ToAssignmentShortInfoDTOList());
        }

        public async Task<ExecutionResult<AssignmentDTO>> GetAssignmentAsync(Guid assignmentId)
        {
            Assignment? assignment = await _assignmentRepository.GetByIdWithContractorAndFixationDefectWithDefectTypeAndPhotosAsync(assignmentId);
            if (assignment is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "AssignmentNotFound", $"Assignment with id {assignmentId} not found!");
            }

            return assignment.ToAssignmentDTO();
        }

        public async Task<ExecutionResult> CreateAssignmentAsync(CreateAssignmentDTO createAssignment, Guid? userId)
        {
            FixationDefect? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAndPhotosAndDefectTypeAsync(createAssignment.FixationDefectId);
            if (fixationDefect is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationDefectNotFound", $"Fixation defect with id {createAssignment.FixationDefectId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwner(fixationDefect.Task!, userId);
            if (checkResult.IsNotSuccess) return checkResult;

            Contractor? contractor = await _contractorRepository.GetByIdAsync(createAssignment.ContractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor defect with id {createAssignment.ContractorId} not found!");
            }

            Assignment assignment = new()
            {
                CreatedDateTime = DateTime.UtcNow,
                DeadlineDateOnly = createAssignment.DeadlineDateOnly,
                FixationDefectId = createAssignment.FixationDefectId,
                ContractorId = createAssignment.ContractorId,
            };

            await _assignmentRepository.AddAsync(assignment);

            ExecutionResult sendResult = await SendCreatedAssignmentNotificationAsync(assignment.Id, contractor, fixationDefect);
            if (sendResult.IsNotSuccess)
            {
                await _assignmentRepository.DeleteAsync(assignment);
                return sendResult;
            }

            return ExecutionResult.SucceededResult;
        }

        private async Task<ExecutionResult> SendCreatedAssignmentNotificationAsync(Guid assignmentId, Contractor contractor, FixationDefect fixationDefect)
        {
            try
            {
                CreatedAssignmentNotification notification = new()
                {
                    AssignmentId = assignmentId,
                    Contractor = contractor.ToContractorDTO(),
                    FixationDefect = fixationDefect.ToFixationDefectDTO(),
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
