using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using KBZ.Utils.Models;
using KBZ.Web.Models;
using System.Web.Security;
using KBZ.Utils.Infrastructure;
using System.Text;
using System.Threading;
using Recaptcha;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security.Principal;

using KBZ.Web.APIModels;
using Serilog;

using KBZ.Administration.Domain.Model;
using KBZ.BLL.Security;


namespace KBZ.Web.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserInformationService _userInformationService;
      
        private APIResponse res = new APIResponse();
        public AccountController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
             
                _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
                
            }
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login(string returnUrl, [FromUri]string trid)
        {
            //var userInfo = System.Threading.Thread.CurrentPrincipal;

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated == true)
            {
                //check if the user is logged in
                var userInformation = Session["UserInformation"];

                if (userInformation != null)
                {
                    //user is logged in, do not show the log in page.
                    //redirect to users home location

                    Type type = userInformation.GetType();
                    PropertyInfo propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == "HomeLocation").FirstOrDefault();
                    if (propertyInfo != null)
                    {
                        var userHomeLocation = propertyInfo.GetValue(userInformation, null);
                        return Redirect(userHomeLocation.ToString());
                    }
                }
            }
            //user is not logged in 
            //Show the login page
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.trid = trid;
            ViewBag.CustomerRegisterUrl =  "/Account/Register";
           // ViewBag.CustomerRegisterUrl = ConfigurationManager.AppSettings["KBZRegistration"] + "/Account/Registration";

            ////////var countryList = _countryService.GetAllCountries();
            ////////Session["CountryList"] = countryList;

            //if ((HttpContext.User as CustomUserPrincipal).UserType != "")
            //{
            //    var UserType = (HttpContext.User as CustomUserPrincipal).UserType;
            //}
            return View();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize]
        public ActionResult WelcomePage()
        {
            return View();
        }
        public ActionResult Requisition()
        {
            return PartialView();
        }
        public ActionResult RequisitionList()
        {
            return PartialView();
        }


  

        //[System.Web.Mvc.HttpGet]
        //[System.Web.Mvc.Authorize]
        //public JsonResult GetRequisitionList()
        //{
        //    var branchId = Session["BranchCode"]!=null? Session["BranchCode"].ToString():null;
        //    var CustomerId = Session["PersonId"] != null ? long.Parse(Session["PersonId"].ToString()) : 0;
        //    var userInfo = Session["UserInformation"] as UserInformationModel;
        //  //  var model =  _PersonalInformationService.GetRequisitionList(branchId,CustomerId, userInfo.UserType);
        //    foreach (var req in model)
        //    {
        //        if (req.CreatedDate != null)
        //        {
        //            req.CreatedDateString = req.CreatedDate.Value.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
    
      



        private void ClearLoginInformation()
        {
            Session["UserInformation"] = null;
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }





 
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ForgotPasswordPost()
        {
            return View();
        }

        public ActionResult ResetPasswordPost()
        {
            return View();
        }

        public ActionResult UnAuthorizedAccess()
        {
            return View();
        }

        public ActionResult LoadMenu()
        {
            return PartialView("_topMenu");
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        public JsonResult ForgotPassword(string email, string responseRecaptcha)
        {
            LoginViewModel model = new LoginViewModel();
          

            var emailHandler = new EmailSender.EmailHandler();

            string userName;
            const string emailSubject = "Forgot password";

            UserInformationModel userInformation = _userInformationService.GetUserByEmail(email);
            if (userInformation != null)
            {
                userName = userInformation.UserName;

                var sendMail = new Thread(delegate ()
                {
                    new EmailSender.EmailHandler().SendEmail(email, userName, emailSubject);
                });
                sendMail.IsBackground = true;
                sendMail.Start();
               
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }



        [System.Web.Mvc.AllowAnonymous]
        public ActionResult VerifyEmailAndMobileNumber(string key)
        {
            return View();
        }



        

        
    }
}