using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Vion.Domain.Entities;

namespace Vion.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendOrderConfirmationEmailAsync(Pedido pedido)
        {
            if (string.IsNullOrEmpty(pedido.EmailCliente))
            {
                _logger.LogWarning($"Pedido {pedido.Id} sem e-mail de cliente definido.");
                return;
            }

            var baseUrl = _configuration["SiteSettings:BaseUrl"] ?? "https://localhost:5001";
            var pedidoUrl = $"{baseUrl.TrimEnd('/')}/Checkout/MeusPedidos";

            var subject = $"Vion - Confirmação do Pedido #{pedido.Id}";
            var body = EmailTemplates.GetOrderConfirmationTemplate(pedido, pedidoUrl);
            await SendEmailAsync(pedido.EmailCliente, subject, body);
        }

        public async Task SendOrderStatusUpdateEmailAsync(Pedido pedido, string novoStatus)
        {
            if (string.IsNullOrEmpty(pedido.EmailCliente))
            {
                _logger.LogWarning($"Pedido {pedido.Id} sem e-mail de cliente definido.");
                return;
            }

            var baseUrl = _configuration["SiteSettings:BaseUrl"] ?? "https://localhost:5001";
            var pedidoUrl = $"{baseUrl.TrimEnd('/')}/Checkout/MeusPedidos";

            var subject = $"Vion - Atualização do Pedido #{pedido.Id}";
            var body = EmailTemplates.GetStatusUpdateTemplate(pedido, novoStatus, pedidoUrl);
            await SendEmailAsync(pedido.EmailCliente, subject, body);
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var subject = "Vion - Redefinição de Senha";
            var body = $@"
                <p>Olá,</p>
                <p>Recebemos uma solicitação para redefinir a senha da sua conta na <strong>Vion</strong>.</p>
                <p>Para criar uma nova senha com segurança, clique no botão abaixo:</p>
                <p style=""margin:24px 0;"">
                    <a href=""{resetLink}"" style=""background-color:#000000;color:#ffffff;padding:12px 24px;border-radius:999px;text-decoration:none;font-weight:bold;"">
                        Redefinir minha senha
                    </a>
                </p>
                <p>Se você não fez essa solicitação, pode ignorar este e-mail com segurança.</p>
                <p style=""margin-top:24px;font-size:12px;color:#666666;"">
                    Por segurança, este link expira após algum tempo e só pode ser usado uma vez.
                </p>";

            await SendEmailAsync(email, subject, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var host = smtpSettings["Host"];
                var port = int.Parse(smtpSettings["Port"] ?? "587");
                var senderEmail = smtpSettings["SenderEmail"];
                var password = smtpSettings["Password"];
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true");

                if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(password))
                {
                    _logger.LogWarning("Email credentials not configured. Skipping email send.");
                    return;
                }

                using (var client = new SmtpClient(host, port))
                {
                    client.Credentials = new NetworkCredential(senderEmail, password);
                    client.EnableSsl = enableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail, "Vion Store"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    
                    // Adicionar Reply-To para evitar filtros de spam e permitir resposta
                    mailMessage.ReplyToList.Add(new MailAddress(senderEmail, "Vion Store"));
                    
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation($"Email sent to {toEmail} successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {toEmail}");
                // We don't throw to avoid breaking the checkout flow
            }
        }
    }
}
