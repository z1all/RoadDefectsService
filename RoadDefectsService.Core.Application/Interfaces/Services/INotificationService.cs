using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<ExecutionResult> SendCreatedAssignmentNotificationAsync(CreatedAssignmentNotificationDTO notification);
    }
}
