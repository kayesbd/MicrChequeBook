using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using log4net;
using KBZ.Utils.Infrastructure;

namespace GITS.Web
{
    public class GITSExceptionHandler : ExceptionFilterAttribute
    {
        public static readonly ILog log4Net =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext filterContext)
        {

                

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }


            if (filterContext.Exception.InnerException != null)
            {

                FileLogger.Info("Error Inner Exception : " + filterContext.Exception.InnerException.Message);
            }

            FileLogger.Info("Error : " + filterContext.Exception.Message);
            FileLogger.Info("Error Stack Trace : " + filterContext.Exception.StackTrace);


            string controllerName = filterContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionContext.ActionDescriptor.ActionName;
           // HandleErrorInfo errorInfo = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred, please try again or contact the administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }
    public class GITSApiExceptionHandler : ExceptionFilterAttribute
    {
       
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
      
            if (actionExecutedContext.Exception.InnerException != null)
            {
               
                FileLogger.Info("Error Inner Exception : " + actionExecutedContext.Exception.InnerException.Message);
            }
         
            FileLogger.Info("Error : " + actionExecutedContext.Exception.Message);
            FileLogger.Info("Error Stack Trace : " + actionExecutedContext.Exception.StackTrace);
          

            // string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            // string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            // HandleErrorInfo errorInfo = new HandleErrorInfo(actionExecutedContext.Exception, controllerName, actionName);
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred, please try again or contact the administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }
}