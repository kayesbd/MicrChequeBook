using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using KBZ.Utils.Infrastructure;

namespace EmailSender
{
    public class SmtpClientGmail : SmtpClient
    {
        public string Sender { get; set; } = "xxxxxxxxxxx@gmail.com";

        public SmtpClient SmtpClient { get; set; }

        public SmtpClientGmail()
        {
            SmtpClient = new SmtpClient
            {
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("askkbzfornewemail@gmail.com", "asdf@1230"),
            };
        }

        public void SendEmail(string mailTo, string emailSubject, string emailBody)
        {
            SmtpClient.Send(Sender, mailTo, emailSubject, emailBody);
        }

        public async Task SendAsyncEmail(string sender, string emailSubject, string emailMessage, string toEmailAddress, string cc)
        {
            var message = new MailMessage
            {
                From = new MailAddress(sender),
                Subject = emailSubject,
                Body = emailMessage,
            };
            message.To.Add(toEmailAddress);

            SmtpClient.SendCompleted += (s, e) =>
            {
                SmtpClient.Dispose();
            };
            await SmtpClient.SendMailAsync(message);
        }

        public void SendEmailWithAttachment(string mailTo, AlternateView alternateView, string subject)
        {
            try
            {
                var mail = new MailMessage();
                mail.AlternateViews.Add(alternateView);
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("xxxxxxxx.com");
                mail.To.Add(mailTo);
                mail.Subject = subject;

                SmtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Info($"Email Sending Failed to {mailTo} and exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Log4NetLogger.Info(ex.InnerException.Message);
                }
            }
        }
    }
}
