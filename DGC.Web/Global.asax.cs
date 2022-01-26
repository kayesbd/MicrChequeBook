using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DGC.BLL;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using KBZ.Web;

namespace KBZ.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CurrentCultureConfig.SetCurrentCulture();
            log4net.Config.XmlConfigurator.Configure();
         
        }

        //protected void TempReceiveMobileRequest()
        //{
        //    NPMobileRequestTemp context = null;
        //    NinjectResolver kernel = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
        //    ICustomerService _customerService = kernel.GetService(typeof(CustomerService)) as CustomerService;

        //    context = new NPMobileRequestTemp();
        //    while (true)
        //    {
        //        var receivedTransaction = context.MobileRequests.Where(x => x.StatusId == 1).ToList();
        //        //Mobile
        //        foreach (var tran in receivedTransaction)
        //        {
        //            var mobileNo = "";
        //            if (tran.Mobile.Length > 11)
        //            {
        //                mobileNo = "+88" + tran.Mobile.Substring(2, 11);
        //            }
        //            var smsStringBuilder = new StringBuilder(tran.SMS);
        //            smsStringBuilder.Replace("+", "");
        //            smsStringBuilder.Remove(0, 4);
        //            var sms = new StringBuilder();
        //            sms.AppendFormat("VA " + smsStringBuilder.ToString());
        //            MobileGatewayController controller = new MobileGatewayController();
        //            controller.SubmitRequest(new TransactionRequestModel() { SMS = sms.ToString(), MobileNumber = mobileNo });
        //            tran.StatusId = 3;
        //            context.SaveChanges();
        //        }
        //        Thread.Sleep(5 * 1000);
        //    }
        //}

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected override IKernel CreateKernel()
        {
            IKernel kernel = BootStrapper.Initialize();
            kernel.Load<WebNinjectModule>();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
            return kernel;
        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}
