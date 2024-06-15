using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Infrastructure.SMTP.Interfaces
{
    public interface ISenderNotificationService
    {
        Task<ExecutionResult> SendAsync(string recipientName, string recipientEmail, string subject, string html, List<PhotoShortInfoDTO>? photos = null);
    }
}
