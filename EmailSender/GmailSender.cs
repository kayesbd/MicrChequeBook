using System;

namespace EmailSender {
 
    /// <summary>
    /// See http://stackoverflow.com/questions/704636/sending-email-through-gmail-smtp-server-with-c
    /// </summary>
    public class GmailSender: SmtpSender {

        public GmailSender() : base("smtp.gmail.com")
        {
            UserName = "kbzmayanmarrototype@gmail.com";
            Password = "asdf@1230";
            EnableSSL = true;
            Port = 587;
        }

        protected override void ConfigureSender(Message message) {
            if (!HasCredentials)
            {
                throw new Exception("Gmail Sender requires account email address and password for authentication");
            }
            base.ConfigureSender(message);
        }
    }
}
