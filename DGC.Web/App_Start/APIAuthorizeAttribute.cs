using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using KBZ.BLL.Security;
using KBZ.Utils.Infrastructure;
using KBZ.Web;

namespace GITS.Web
{

    public class APIAuthorizeAttribute : AuthorizeAttribute
    {
        private  IUserInformationService _userInformationService;
        //public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        //{
        //    if ((actionContext.ControllerContext.Controller is UserController))
        //        base.OnAuthorization(actionContext);
        //}

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //userinformationservice dependency to prevent multiple login
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;



            var isAuthorised = base.IsAuthorized(actionContext);

            if (isAuthorised)
            {

                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var identity = new GenericIdentity(ticket.Name);
                string userData = ticket.UserData;
                string Id = "";
                

                if (userData.Contains("="))
                {
                    string[] data = userData.Split('=');
                    if (data != null && data.Length > 3)
                    {
                        string Email = data[0];
                                 Id = data[1];
                        string FullName = data[2];
                        string Role = data[3];
                        string PersonId = data[4];
                        string OrganizationId = data[5];
                        string exchangeUID = "", exchangePWD = "", exchangeDomain = "";
                        if (data.Length > 6)
                            exchangeUID = data[6];
                        if (data.Length > 7)
                            exchangePWD = data[7];

                        if (data.Length > 8)
                            exchangeDomain = data[8];

                        string ClientId = data[9];
                        string UserType = data[10];
                        string UTCTimeId = data[12];
                        string mobileNumber = data[13];

                        var principal = new CustomUserPrincipal(identity, long.Parse(Id), Email, Role, FullName,
                            long.Parse(PersonId), long.Parse(OrganizationId), exchangeUID, "", exchangeDomain, long.Parse(ClientId), UserType, UTCTimeId,mobileNumber);
                        HttpContext.Current.User = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }
              //  AuthUtil.SessionHandler(HttpContext.Current);
                AuthUtil.SessionUserHandler(HttpContext.Current);

               //Update user session timeout time and reference in database 
                //To prevent Multiple login
                //generate session timeout time.
                var sessTime = System.DateTime.UtcNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
                _userInformationService.UpdateSessionForUser(long.Parse(Id), sessTime);
                

            }
        }
    }
}