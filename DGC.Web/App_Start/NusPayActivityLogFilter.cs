using System.Linq;
using System.Web.Http.Filters;
using System.Web.Mvc;
using NusPay.BLL.Common;
using NusPay.Common.Domain.Model;
using NusPay.Web.App_Start;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace NusPay.Web
{
    public class NusPayActivityLogging : ActionFilterAttribute
    {
        public string ActivityType { get; set; }
        public IActivityService recentActivity = null;
        public ActivityModel model = null;

        public string ActivityDescription { get; set; }



        public NusPayActivityLogging(string activity)
        {
            model = new ActivityModel();
            model.ActivityTypeName = activity;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
            this.Logging(filterContext);
        }
        private void Logging(ActionExecutingContext filterContext)
        {

            model.Action = filterContext.ActionDescriptor.ActionName;
            model.Controller = filterContext.Controller.ToString();
            model.ActivityDescription = this.ActivityDescription;
            if (filterContext.RequestContext.RouteData != null)
            {
                model.Parameter = filterContext.RequestContext.RouteData.Values.FirstOrDefault().Value.ToString();
            }
            if (filterContext.HttpContext.Request.Url != null)
            {
                model.ActivityUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
            }
            WebUtility.Log(model);
        }
    }

    public class NusPayActivityLogFilter : ActionFilterAttribute
     {
         public string ActivityType { get; set; }
         public IActivityService recentActivity = null;
         public ActivityModel model = null;
       
         public string ActivityDescription { get; set; }

        
       
         public NusPayActivityLogFilter(string activity)
         {
              model = new ActivityModel();
              model.ActivityTypeName = activity;
         }
       
         
          public override void OnActionExecuted(ActionExecutedContext filterContext)
          {
           this.Log(filterContext);
              //Task.Factory.StartNew(() => Log(filterContext));
          }

        private void Log(ActionExecutedContext filterContext)
        {

            model.Action = filterContext.ActionDescriptor.ActionName;
            model.Controller = filterContext.Controller.ToString();
            model.ActivityDescription = this.ActivityDescription;
            if (filterContext.RequestContext.RouteData != null)
            {
                model.Parameter = filterContext.RequestContext.RouteData.Values.FirstOrDefault().Value.ToString();
            }
            if (filterContext.HttpContext.Request.Url != null)
            {
                model.ActivityUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
            }
            WebUtility.Log(model);
        }


     }
    public class NusPayHttpActivityLogger : System.Web.Http.Filters.ActionFilterAttribute
    {
        public string ActivityType { get; set; }
        public IActivityService recentActivity = null;
        public ActivityModel model = null;

        public string ActivityDescription { get; set; }



        public NusPayHttpActivityLogger(string activity)
        {
            model = new ActivityModel();
            model.ActivityTypeName = activity;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            this.Log(actionExecutedContext);
            //Task.Factory.StartNew(() => Log(filterContext));
        }


        private void Log(HttpActionExecutedContext filterContext)
        {

            model.Action = filterContext.ActionContext.ActionDescriptor.ActionName;
            model.Controller = filterContext.ActionContext.ControllerContext.Controller.ToString();
            model.ActivityDescription = this.ActivityDescription;
            //if (filterContext.RequestContext.RouteData != null)
            //{
            //    model.Parameter = filterContext.RequestContext.RouteData.Values.FirstOrDefault().Value.ToString();
            //}
            if (filterContext.Request.RequestUri!=null)
            {
                model.ActivityUrl = filterContext.Request.RequestUri.AbsoluteUri;
            }
            WebUtility.Log(model);
        }


    }
}