using System;
using System.Configuration;
using System.Web.Mail;

namespace KBZ.Utils.Infrastructure
{
    public class EmailHandler
    {
        public static void SendMail(string Sender, string Subject, string MailConent, string[] Attachments, string Recipient, string Cc)
        {

            try
            {


                if (Recipient.Trim().Length > 0)
                {
                    MailMessage Content = new MailMessage();

                    if (ConfigurationSettings.AppSettings["emailpassword"]!=null)
                    {
                        Content.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;
                        Content.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                        Content.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = ConfigurationSettings.AppSettings["MailServer"];
                        Content.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = ConfigurationSettings.AppSettings["smtpPort"];
                        Content.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = ConfigurationSettings.AppSettings["emailuser"];
                        Content.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = ConfigurationSettings.AppSettings["emailpassword"];
                    }
                    else
                        SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["MailServer"];

                    Content.From = Sender;
                    Content.Subject = Subject;
                    Content.BodyFormat = MailFormat.Html;
                    Content.Body = MailConent;

                    if (Attachments != null && Attachments.Length != 0)
                    {
                        for (int i = 0; i < Attachments.Length; i++)
                        {
                            if (Attachments[i].Trim().Length > 0)
                            {
                                Content.Attachments.Add(new MailAttachment(Attachments[i].Trim()));
                            }
                        }
                    }
                    Content.To = Recipient;
                    if (Cc != null && Cc.Trim().Length > 0) Content.Cc = Cc;
                    //					SmtpMail.SmtpServer =ConfigurationSettings .AppSettings ["MailServer"];
                    SmtpMail.Send(Content);
                }

            }
            catch (Exception e)
            {
                if (string.Compare("Could not access 'CDO.Message' object.", e.Message, true) == 0)
                {
                    throw new Exception("Unable to Send Mail");
                }
                throw e;

            }

        }
        public static void SendMail(string Sender, string Subject, string MailConent, string Recipients, string Cc)
        {

            SendMail(Sender, Subject, MailConent, null, Recipients, Cc);

        }

    }
}
