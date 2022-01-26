using Microsoft.Owin;
using KBZ.Utils.Infrastructure;
using Owin;
using Serilog;

[assembly: OwinStartupAttribute(typeof(KBZ.Web.Startup))]
namespace KBZ.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
          

            FileLogger.Info("Hello this is testing from client pc. If localhost nothing will be a problem");
            //FileLogger.Logger = new LoggerConfiguration()
            //.WriteTo.Seq("http://localhost:5341",
            // bufferBaseFilename: @"C:\MyApp\Logs\myapp")
            //.CreateLogger();
        }
    }
}
