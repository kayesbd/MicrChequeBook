using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KBZ.Utils.Infrastructure;

namespace EmailSender
{
    public class EmailHandler
    {
        private static string _link = "";
        private static string _activationLink = "";
        public static SmtpClientGmail SmtpClientGmail { get; set; }

        public EmailHandler()
        {
            SmtpClientGmail = new SmtpClientGmail();
        }

        public static async Task SendAsyncEmail(string sender, string emailSubject, string emailMessage, string toEmailAddress, string cc)
        {
            try
            {
                await SmtpClientGmail.SendAsyncEmail(sender, emailSubject, emailMessage, toEmailAddress, cc);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Info($"Email Sending Failed for the user and exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Log4NetLogger.Info(ex.InnerException.Message);
                }
            }
        }

        public void SendEmail(string mailTo, string emailSubject, string emailBody)
        {
            try
            {
                SmtpClientGmail.SendEmail(mailTo, emailSubject, emailBody);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Info($"Email Sending Failed for exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Log4NetLogger.Info(ex.InnerException.Message);
                }
            }
        }

        public static void SendEmail(string mailTo, string fullName, string emailTemplate, string emailBody)
        {
            try
            {
                GmailSender gmail = new GmailSender();

                Message message = new Message
                {
                    From = gmail.UserName,
                    To = mailTo
                };

                var bytes = Encoding.UTF8.GetBytes(mailTo);
                string base64 = Convert.ToBase64String(bytes);

                switch (emailTemplate)
                {
                    case "Forgot password":
                        message.Body = "<p>" + fullName + " please click the following link to reset your password: <a href='" + _link + "</a>";
                        message.Body += "If you did not request a password reset you do not need to take any action.</p>";
                        break;
                    case "Registration":
                        _activationLink = _activationLink + base64 + "";
                        message.Body = "";
                        message.Body += "Hello " + fullName + ",";
                        message.Body += "<p>Thank you for registering with us.</p>";
                        message.Body += "<p>Please click on the following link to verify your email address.</p>";
                        message.Body += "<p><a href='" + _activationLink + "'>Verify email</a></p>";
                        break;
                    case "Transaction":
                        message.Body = "Use " + emailBody;
                        break;
                    case "QR-Code Generation":
                        message.Body = emailBody;
                        message.Body += "<p><img src='data:image/jpeg;base64, " + fullName + "'/></p>";
                        break;
                    case "TransactionFromTerminals":
                        message.Body = emailBody;
                        break;
                    case "TransactionComplete":
                        message.Body = "<p>" + fullName + " Your transaction has been successfully completed.</p>";
                        break;
                    case "Generate PIN":
                        message.Body = "<p>" + "Your New PIN is:" + emailBody + "</p>";
                        break;
                    case "Generate PIN For Registration":
                        message.Body = "";
                        message.Body += "Hello " + fullName + ",";
                        message.Body += "<p>Thank you for registering with us.</p>";
                        message.Body += "<p>" + "Your  PIN is:" + emailBody + "</p>";
                        break;
                    case "Generate Merchant Security Key":
                        message.Body = "";
                        message.Body += "<p>Thank you for connect with us.</p>";
                        message.Body += "<p>" + "Your  Security Key is:" + emailBody + "</p>";
                        break;
                    case "OTP":
                        message.Body = "Your OTP is:" + emailBody;
                        break;
                    case "Change Password":
                        message.Body = "<p>" + fullName + " Your password changed successfully.</p>";
                        break;
                    case "Registration Completed":
                        message.Body = "<p>" + fullName + " Your registration has been completed.</p>";
                        break;
                }

                message.Subject = emailTemplate;
                gmail.Send(message);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Info($"Email Sending Failed for the user {fullName} and exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Log4NetLogger.Info(ex.InnerException.Message);
                }
            }
        }

        public static string HashResetParams(string username, string guid)
        {
            byte[] bytesofLink = Encoding.UTF8.GetBytes(guid + username);
            MD5 _md5 = new MD5CryptoServiceProvider();
            string HashParams = BitConverter.ToString(_md5.ComputeHash(bytesofLink));

            return HashParams;
        }
    }
}
