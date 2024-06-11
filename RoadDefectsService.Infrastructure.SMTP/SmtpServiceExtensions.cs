using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Infrastructure.SMTP.Interfaces;
using RoadDefectsService.Infrastructure.SMTP.Services;

namespace RoadDefectsService.Infrastructure.SMTP
{
    public static class SmtpServiceExtensions
    {
        public static IServiceCollection AddSmtpServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, BusNotificationService>();
            services.AddScoped<ISenderNotificationService, EmailSenderNotificationService>();

            return services;
        }
    }
}
