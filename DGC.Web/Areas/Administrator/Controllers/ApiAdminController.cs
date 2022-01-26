using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Ajax.Utilities;

using KBZ.Utils.Infrastructure;
using KBZ.Web.APIModels;
using System.Threading;
using System.Web.Security;
using KBZ.Utils;
using KBZ.BLL.Security;

namespace KBZ.Web.Areas.Administrator.Controllers
{
    public class ApiAdminController : ApiController
    {
        
       
        private readonly IUserInformationService _userInformationService;
       
   


        public ApiAdminController(IUserInformationService userInformationService)
        {
            _userInformationService = userInformationService;
        }

   

      


        [HttpGet]
        [System.Web.Mvc.Authorize]
        public APIResponse GetInfo()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            return new APIResponse { Success = true, Id = 0, ErrorMessage = "", ModelValue = cookie.Value };
        }

       


    }
}
