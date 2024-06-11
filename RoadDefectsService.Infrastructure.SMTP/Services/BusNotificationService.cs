using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Infrastructure.SMTP.Services
{
    public class BusNotificationService : INotificationService
    {
        public Task<ExecutionResult> SendCreatedAssignmentNotificationAsync(CreatedAssignmentNotification notification)
        {
            return Task.FromResult(ExecutionResult.SucceededResult);
        }
    }
}
