using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Infrastructure.SMTP.Configurations;
using RoadDefectsService.Infrastructure.SMTP.Interfaces;
using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Application.Configurations.FileStorage;
using MailKit.Net.Smtp;
using MimeKit;

namespace RoadDefectsService.Infrastructure.SMTP.Services
{
    public class GmailSenderNotificationService : ISenderNotificationService
    {
        private readonly GmailSmtpOptions _smtpOptions;
        private readonly FileStorageOptions _fileStorageOptions;

        public GmailSenderNotificationService(IOptions<GmailSmtpOptions> options, IOptions<FileStorageOptions> fileStorageOptions)
        {
            _smtpOptions = options.Value;
            _fileStorageOptions = fileStorageOptions.Value;
        }

        public async Task<ExecutionResult> SendAsync(string recipientName, string recipientEmail, string subject, string html, List<PhotoShortInfoDTO>? photos)
        {
            MimeMessage message = new();

            message.From.Add(new MailboxAddress(_smtpOptions.SenderName, _smtpOptions.SenderEmail));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = subject;

            BodyBuilder bodyBuilder = new() { HtmlBody = html };
            foreach (var photo in photos ?? new())
            {
                string path = Path.Combine(_fileStorageOptions.StoragePath, photo.PathName);
                if (File.Exists(path))
                {
                    await bodyBuilder.Attachments.AddAsync(path);
                }
            }
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.UseSsl);
                await client.AuthenticateAsync(_smtpOptions.UserEmail, _smtpOptions.Password);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }

            return ExecutionResult.SucceededResult;
        }
    }
}
