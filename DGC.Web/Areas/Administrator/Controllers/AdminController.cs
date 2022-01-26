using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using KBZ.Administration.Domain.Model;
using KBZ.BLL.Security;


//using ZXing;

namespace KBZ.Web.Areas.Administrator.Controllers
{
    public class AdminController : Controller
    {
        #region Private Variables

     
        private UserInformationService _userInformationService;
        //private TransactionInformationService _transactionService;


        private readonly UserInformationModel _loginUserInformation;

        #endregion

        public AdminController()
        {
            if (System.Web.HttpContext.Current.Session["UserInformation"] != null)
            {
                _loginUserInformation = (UserInformationModel)System.Web.HttpContext.Current.Session["UserInformation"];
            }
        }

   

        #region User

        public ActionResult UserDetails()
        {
            return View();
        }
        public ActionResult DeleteUserDetails()
        {
            return View();
        }
        public ActionResult UserList()
        {
            return View();
        }
        public ActionResult UserPasswordChange()
        {
            return View();
        }

        //[System.Web.Mvc.HttpPost]
        //public APIResponse ResetPassword(UserInformationModel userInformation)
        //{
        //    _userInformationService.ChangePassword(userInformation);
        //    return new APIResponse { Success = true, Id = userInformation.Id, ErrorMessage = "", ModelValue = userInformation };
        //}

        [System.Web.Mvc.HttpPost]
        public JsonResult GetUserList()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
            var list = _userInformationService.GetUserList();
            IQueryable<UserInformationModel> result = list.AsQueryable();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

    

     

        


       

       

    }
}       