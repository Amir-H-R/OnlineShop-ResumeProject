using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MailSender
{
    public interface IMailSenderService
    {
        Task Execute(string userEmail, string body, string subject);
    }
    public class EmailSenderService:IMailSenderService
    {
        public Task Execute(string userEmail, string body, string subject)
        {
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 1000000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("amirfighter.1383@gmail.com", "qecuidbopxkhbgkg");

            MailMessage mailMessage = new MailMessage("amirfighter.1383@gmail.com", userEmail, subject, body);

            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            smtpClient.Send(mailMessage);
            return Task.CompletedTask;
        }
    }
}
