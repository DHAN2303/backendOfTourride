using AllrideApiService.Services.Abstract.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace AllrideApiService.Services.Concrete.Mail
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;
        readonly ILogger<MailService> _logger;
        public MailService(IConfiguration configuration, ILogger<MailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        // Aktivayon kodunu string olarak gönder
        public async Task<bool> SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            try
            {
                await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(" MAIL SERVICE ERROR SendMessageAsync One Person LOG ERROR: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            try
            {
                MailMessage mail = new();
                mail.IsBodyHtml = isBodyHtml;
                foreach (var item in tos)
                {
                    mail.To.Add(item);
                }
                mail.Subject = subject;
                mail.Body = body;
                mail.From = new(_configuration["Mail:Username"], "TOURIDE", System.Text.Encoding.UTF8);

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = _configuration["Mail:Host"]; // Bizim host bilgimiz yok ayarlanması gerekiyor
                await smtp.SendMailAsync(mail);
                return true;     
            }
            catch (Exception ex)
            {
                _logger.LogError(" MAIL SERVICE ERROR SendMessageAsync LOG ERROR: " + ex.Message);
                return false;
            }
            
        }

    }
}
