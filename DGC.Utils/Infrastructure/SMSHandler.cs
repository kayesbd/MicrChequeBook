using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
namespace KBZ.Utils.Infrastructure
{
    public class SMSHandler:ISMSHandler
    {
        private string urlBase = "";
        private string urlOther = "";
        private string param =  "";     
        private string userId;
        private string password;

        public SMSHandler()
        {
            urlBase = ConfigurationManager.AppSettings["SMSBaseUrl"];
            urlOther = ConfigurationManager.AppSettings["SMSSentUrl"];
            param = "query?to={0}&text={1}";   
        }

        public void SendRegistratoinNotification(SMSModel sms)
        {
            var msgSent = new Task(() => SendSMS(sms));
            msgSent.Start();
            msgSent.Wait();
        }

        public void SendSMS(SMSModel sms)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(urlBase + urlOther + param, sms.MobileNumber, sms.Message);

            try
            {
                var webClient = new WebClient();
                Stream stream = webClient.OpenRead(sb.ToString());
                var sreader = new StreamReader(stream);
                sreader.ReadToEnd();
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    throw;
                }
            }
            //var Client = new WebClient();
            //string replyData = Client.DownloadString(sb.ToString());
            //byte[] strResult = Client.DownloadData(sb.ToString());
            //var returString = System.Text.Encoding.UTF8.GetString(strResult);
        }

        public void SendVoice(SMSModel sms)
        { 
        
        }
    }
}
