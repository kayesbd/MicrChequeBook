using KBZ.Utils.Infrastructure;
using System.Web.Mvc;
using System.Web.Http;
using System;
using System.Reflection;
using System.Linq;
using KBZ.BLL.Security;

namespace KBZ.Web.Controllers
{

    public class HomeController : Controller
    {

      
        public ActionResult Index(string returnUrl)
        {
            long userId = 0;
           
            if ((HttpContext.User as CustomUserPrincipal) != null)
            {
                userId = (HttpContext.User as CustomUserPrincipal).Id;
                ViewBag.Id = (HttpContext.User as CustomUserPrincipal).ClientId;


                var userSession = Session["UserInformation"];
                if (userSession == null)
                {
                    return RedirectToAction("LogOff", "Account");
                }
                else
                {
                    Type type = userSession.GetType();
                    PropertyInfo propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == "HomeLocation").FirstOrDefault();
                    if (propertyInfo != null)
                    {
                        Uri MyUrl = Request.Url;
                        var appUrl = Request.Url.PathAndQuery;
                        var userHomeLocation = propertyInfo.GetValue(userSession, null);
                        //return Redirect(userHomeLocation.ToString());
                    }
                }
               

                //return Redirect(userHomeLocation);
            }
            else
            {
                var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
                var service = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
                var user = service.GetUser(userId);
                //if(user!=null)
                //{
                //    ViewBag.Id = user.ClientId;
                //}
                
            }


            if (ViewBag.UserTypeId == null)
            {
                ViewBag.UserTypeId = this.HttpContext.Session["UserTypeId"];
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        //Test
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        //public ActionResult ErrorPageNotFound()
        //{
        //    return View();
        //    //return "Page Not Found";
        //}
        
        public string ErrorPageNotFound()
        {
           // return View();
            return "Page Not Found";
        }
        

        public ActionResult Customer()
        {
            ViewBag.Message = "Test Customer";
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.Message = "Log in";
            return View();
        }

        public ActionResult Registration()
        {
            ViewBag.Message = "Log in ";
            return View();
        }
        public ActionResult LoginPartial()
        {

            return PartialView("_LoginPartial");
        }
    }
}