using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


using System;
using KBZ.Utils.App_Start;
using KBZ.Administration.Domain.Model;
using KBZ.Utils.Infrastructure;
using KBZ.Web.Controllers;

namespace GITS.Web
{
    public class LogonAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var currentType = filterContext.Controller.GetType();
            if (!(filterContext.Controller is AccountController))
            {
                base.OnAuthorization(filterContext);
            }
        }

        public void AuthorizeManually(System.Web.HttpContextBase httpContext)
        {
            this.AuthorizeCore(httpContext);
        }

        //private void AuthenticateByUrl(HttpContextBase httpContext, string[] data, string name)
        //{
        //    // also check is module level permission
        //    if (permittedPaths != null)
        //    {
        //        bool homePageUrl = false;
        //        bool errorPageUrl = false;
        //        UserInformationModel userModel = AuthUtil.GetLoggedInUser(data[1]);

        //        if (userModel.UserType == UserType.SuperAdmin.ToString())
        //        {
        //            return;
        //        }

        //        string homeUrl = "";
        //        if (httpContext.Request.RawUrl.IndexOf("/Home/ErrorPageNotFound") > -1)
        //            errorPageUrl = true;
        //        if (userModel.UserType == UserType.BankAdmin.ToString())
        //        {
        //            if (httpContext.Request.RawUrl.IndexOf("Client/Customer/Home") > -1)
        //                homePageUrl = true;
        //        }
               
        //        else if (userModel.UserType == UserType.BankAdmin.ToString())
        //        {
        //            if (httpContext.Request.RawUrl.IndexOf("Administrator/Bank/Home") > -1)
        //                homePageUrl = true;
        //        }

        //        if (errorPageUrl == false && homePageUrl == false && httpContext.Request.RawUrl != "/")
        //        {
        //            string requestAction, requestedController, requestedArea, fullUrl = "";

        //            if (!String.IsNullOrEmpty(httpContext.Request.RawUrl))
        //            {
        //                if (!httpContext.Request.IsAjaxRequest())
        //                {
        //                    string[] strUrlParts = httpContext.Request.RawUrl.Split(Convert.ToChar("/"));
        //                    if (strUrlParts.Length > 3)
        //                    {
        //                        requestedArea = strUrlParts[1];
        //                        requestedController = strUrlParts[2];
        //                        requestAction = strUrlParts[3];
        //                        fullUrl = "/#/" + requestedArea + "/" + requestedController + "/" + requestAction;
        //                    }
        //                    else if(strUrlParts.Length > 2)
        //                    {
        //                        requestedArea = strUrlParts[1];
        //                        requestedController = strUrlParts[2];
                                
        //                        fullUrl = "/#/" + requestedArea + "/" + requestedController;
        //                    }
                          
        //            }
        //        }
        //    }
        //}

        private string GetRouteData(System.Web.HttpContextBase httpcontext, string key)
        {
            var routeValues = httpcontext.Request.RequestContext.RouteData.Values;
            if (routeValues != null)
            {
                if (routeValues.ContainsKey(key))
                {
                    return HttpContext.Current.Request.RequestContext.RouteData.Values[key].ToString();
                }
            }
            return "";
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {

            var isAuthorised = base.AuthorizeCore(httpContext);
            if (isAuthorised)
            {
                var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var identity = new GenericIdentity(ticket.Name);
                string userData = ticket.UserData;

                string[] data = null;
                if (userData.Contains("="))
                {
                    data = userData.Split('=');
                    if (data.Length > 3)
                    {
                        string email = data[0];
                        string id = data[1];
                        string fullName = data[2];
                        string role = data[3];
                        string personId = data[4];
                        string organizationId = data[5];
                        string exchangeUID = "", exchangePWD = "", exchangeDomain = "";
                        if (data.Length > 6)
                            exchangeUID = data[6];
                        if (data.Length > 7)
                            exchangePWD = data[7];
                        if (data.Length > 8)
                            exchangeDomain = data[8];
                        string clientId = data[9];
                        string userType = data[10];
                        string UTCTimeId = data[12];
                        string mobileNumber = data[13];
                        var principal = new CustomUserPrincipal(identity, int.Parse(id), email, role, fullName,
                            int.Parse(personId),
                            int.Parse(organizationId), exchangeUID,
                            "", exchangeDomain, long.Parse(clientId), userType, UTCTimeId, mobileNumber);
                        httpContext.User = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }

                /////========================= Checking Role Based Permission =============================================
               // this.AuthenticateByUrl(httpContext, data, identity.Name);
                ////========================= Checking Role Based Permission =============================================


                //=========update session variable ==========
                //AuthUtil.SessionHandler(HttpContext.Current);
                AuthUtil.SessionUserHandler(HttpContext.Current);

                //var SESSID = httpContext.Request.Cookies["ASPSESSID"];
                //if (null == SESSID)
                //{
                //    SESSID = new HttpCookie("ASPSESSID");
                //}
                //SESSID.Expires = DateTime.UtcNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
                //SESSID.Value = "Session";
                //HttpContext.Current.Response.Cookies.Set(SESSID);
                //===========================================
            }

            return isAuthorised;
        }
    }
}