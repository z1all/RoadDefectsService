﻿using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
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

        public MetricsService(
            IFixationWorkRepository fixationWorkRepository, IAssignmentRepository assignmentRepository, 
            ICoordinateFixationDefectRepository coordinateFixationDefectRepository, IReportService reportService)
        {
            _coordinateFixationDefectRepository = coordinateFixationDefectRepository;
            _fixationWorkRepository = fixationWorkRepository;
            _reportService = reportService;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<ExecutionResult<List<CoordinateFixationDefectDTO>>> GetCoordinatesFixationsDefectsAsync(CoordinatesFilter filter)
        {
            List<CoordinateFixationDefect> coordinates = await _coordinateFixationDefectRepository.GetAllByFilterAsync(filter);

            return coordinates.ToCoordinateFixationDefectDTOList();
        }

        public async Task<ExecutionResult<ReportDTO>> GetWorkReportAsync(Guid fixationWorkId)
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

            GenerateWorkReportDTO generateWorkReport = new()
            {
                AssignmentId = assignment.Id,
                Contractor = assignment.Contractor!.ToContractorDTO(),
                FixationDefect = assignment.FixationDefect!.ToFixationDefectDTO(),
                FixationWork = fixationWork.ToFixationWorkDTO()
            };

            return _reportService.GenerateReport(generateWorkReport);
        }
    }
}
