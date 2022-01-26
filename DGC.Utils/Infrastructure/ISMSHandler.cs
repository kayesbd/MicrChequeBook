namespace KBZ.Utils.Infrastructure
{
    public interface ISMSHandler
    {
         void SendRegistratoinNotification(SMSModel sms);
         void SendSMS(SMSModel sms);
         void SendVoice(SMSModel sms);
    }

}
