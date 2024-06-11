using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Infrastructure.SMTP.Interfaces;

namespace RoadDefectsService.Infrastructure.SMTP.Services
{
    public class BusNotificationService : INotificationService
    {
        private readonly ISenderNotificationService _senderNotificationService;

        public BusNotificationService(ISenderNotificationService senderNotificationService)
        {
            _senderNotificationService = senderNotificationService;
        }

        public Task<ExecutionResult> SendCreatedAssignmentNotificationAsync(CreatedAssignmentNotificationDTO notification)
        {
            string title = "Назначение на выполнение работ по устранению дорожного дефекта";
            string styles =
                ".content > * {" +
                "    padding: 0 15px;" +
                "}" +
                ".defectInfoContainer *:not(:first-child) {" +
                "    border-top: 1px solid #BFBFBF;" +
                "}" +
                ".defectInfoContainer > * {" +
                "    margin: 0 10px;" +
                "    padding: 3px 0;" +
                "}";
            string body =
               $"<p>Добрый день, {notification.Contractor.ContractorFullName}</p>" +
               $"<p>Ваша организация {notification.Contractor.OrganizationName} была выбрана для устранения дорожного дефекта!</p>" +
                "<p>Ниже указана информация о дефекте, а во вложении находятся фотографии дефекта.</p>" +
                "<div class=\"defectInfoContainer\">" +
               $"   <p>Адрес: {notification.FixationDefect.ExactAddress}</p>" +
               $"   <p>Количество поврежденного полотна: {notification.FixationDefect.DamagedCanvasSquareMeter} метров</p>" +
               $"   <p>Тип дефекта: {notification.FixationDefect.DefectTypeName}</p>" +
               $"   <p>Идентификатор назначения работ: {notification.AssignmentId} (при следующем обращении укажите его)</p>" +
                "</div>";

            return _senderNotificationService.SendAsync(
                notification.Contractor.ContractorFullName, 
                notification.Contractor.Email,
                title,
                GenerateLayout(title, body, styles), 
                notification.FixationDefect.Photos);
        }

        private string GenerateLayout(string title, string body, string? styles = null)
        {
            return "<!DOCTYPE html>" +
                "<html lang=\"en\">" +
                "<head>" +
                "   <meta charset=\"UTF-8\">" +
                "   <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                "   <title>Email</title>" +
                "   <style>" +
                "       body {" +
                "            font-family: Arial, sans-serif;" +
                "            line-height: 1.6;" +
                "            margin: 0;" +
                "            padding: 0;" +
                "       }" +
                "       .container {" +
                "            width: 80%;" +
                "            margin: auto;" +
                "            padding: 20px;" +
                "            border: 1px solid #ddd;" +
                "            border-radius: 10px;" +
                "       }" +
                "       .header, .footer {" +
                "            background-color: #f8f8f8;" +
                "            padding: 10px;" +
                "            text-align: center;" +
                "       }" +
                "       .content {" +
                "            margin: 20px 0;" +
                "       }" +
               $"      {styles}" +
                "    </style>" +
                "</head>" +
                "<body>" +
                "    <div class=\"container\">" +
                "        <div class=\"header\">" +
               $"            <h2>{title}</h2>" +
                "        </div>" +
                "        <div class=\"content\">" +
               $"            {body}" +
                "        </div>" +
                "        <div class=\"footer\">" +
                "            <p>&copy; 2024 Road defects</p>" +
                "        </div>" +
                "    </div>" +
                "</body>" +
                "</html>";
        }
    }
}
