using System.Web.Mvc;

//using log4net;
//using log4net.Config;

namespace NusPay.Web.Controllers
{
    public class BaseController : Controller
    {

        public BaseController()
        {
            //var custom = User as CustomUserPrincipal;
            //if (custom == null)
            //{
            //    LogonAuthorizeAttribute logon = new LogonAuthorizeAttribute();
            //    var context =(System.Web.HttpContextBase) System.Web.HttpContext.Current ;
            //    logon.AuthorizeManually();
            //    System.Web.HttpContextBase 
            //}
        }

        protected void SetAuthInfo(int Id, string Email, string FullName, string Role, int PersonId, int OrganizationId
            , string exchangeUID, string exchangePWD, string exchangeDomain, long clientId, string userType, int? fileManagerPermission, string UTCTimeId,string mobileNumber)
        {
            AuthUtil.SetAuthInfo(Id, Email, FullName, Role, PersonId, OrganizationId, exchangeUID, exchangePWD,
                exchangeDomain, clientId, userType, fileManagerPermission, UTCTimeId, mobileNumber);
        }

    }
}
