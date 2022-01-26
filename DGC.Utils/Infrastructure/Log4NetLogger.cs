using System;
using System.Threading.Tasks;
using log4net;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace KBZ.Utils.Infrastructure
{
    public static class Log4NetLogger
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(Log4NetLogger));

        static Log4NetLogger()
        {
        }

        public static void Error(object msg)
        {
            Task.Run(() =>  FileLogger.Info(msg.ToString()));
        }

        public static void Error(object msg, Exception ex)
        {
            FileLogger.Info(msg.ToString());
        }

        public static void Error(Exception ex)
        {
            FileLogger.Info(ex.Message);
        }

        public static void Info(object msg)
        {
            Task.Run(() =>  FileLogger.Info(msg.ToString()));
        }

        public static void Init()
        {
        }
    }
}
