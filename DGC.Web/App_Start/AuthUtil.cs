using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;

using System.Web.Helpers;
using KBZ.Administration.Domain.Model;
using KBZ.Utils.Caching;
using KBZ.Utils.Infrastructure;

namespace GITS.Web
{
    public class UserIdentity : FormsIdentity
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public long PersonId { get; set; }
        public long OrganizationId { get; set; }
        public string ExchangeUID { get; set; }
        public string ExchangePwd { get; set; }
        public string ExchangeDomain { get; set; }
        public int? FileManagerPermission { get; set; }
        public long ClientId { get; set; }
        public long ClientTypeId { get; set; }

        public UserIdentity(FormsAuthenticationTicket ticket)
            : base(ticket)
        {
            if (ticket.UserData != null && ticket.UserData.IndexOf("=") != -1)
            {
                string[] dataSections = ticket.UserData.Split('=');
                Id = long.Parse(dataSections[1]);
                Email = dataSections[0];
                FullName = dataSections[2];
                Role = dataSections[3];
                PersonId = long.Parse(dataSections[4]);
                OrganizationId = long.Parse(dataSections[5]);
                ExchangeUID = dataSections[6];
                ExchangePwd = dataSections[7];
                ExchangeDomain = dataSections[8];
                ClientId = long.Parse(dataSections[9]);
                ClientTypeId = long.Parse(dataSections[10]);
                FileManagerPermission = int.Parse(dataSections[11]);
            }
        }
    }

    public class AuthUtil
    {
        public static void SetAuthInfo(long Id, string email, string FullName, string role, long personId,
            long organizationId
            , string exchangeUID, string exchangePWD, string exchangeDomain, long clientId, string userType,
            long? fileManagerPermission, string UTCTimeId,string mobileNumber)
        {
            if (HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            }

          string fullName = FullName;
            //string fullName = "";
            //role = role;
            exchangePWD = "test";
            exchangeDomain = "hard-own.com";
            exchangeUID = "test";

            var ticket = new FormsAuthenticationTicket(
                1,
                fullName,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                string.Format("{0}={1}={2}={3}={4}={5}={6}={7}={8}={9}={10}={11}={12}={13}", email, Id, FullName, role, personId,
                    organizationId, exchangeUID,
                    "", exchangeDomain, clientId, userType, "1", UTCTimeId,mobileNumber));

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes)
            };

            //impleent database
            // session id
            // Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes)
            //
            //var ASPSESSID = new HttpCookie("ASPSESSID")
            //{
            //    HttpOnly = true,
            //    Secure = FormsAuthentication.RequireSSL,
            //    Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes)
            //};


            var identity = new GenericIdentity(ticket.Name);
            var principal = new CustomUserPrincipal(identity, Id, email, role, FullName, personId, organizationId
                , exchangeUID, exchangePWD, exchangeDomain, clientId, userType, UTCTimeId,mobileNumber);
            HttpContext.Current.User = principal;
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        public static string GetAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }
        public static void SessionHandler(HttpContext httpContext)
        {
            //=========update session variable ==========
            var SESSID = httpContext.Request.Cookies["ASPSESSID"];
            if (null == SESSID)
            {
                SESSID = new HttpCookie("ASPSESSID");
            }
            var sessTime = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
            // This -5 seconds for safety, if a client call ajax and then client side session
            //is not finished but whe the request comes to server, session already expires
            SESSID.Expires = sessTime.AddSeconds(-5);
            SESSID.Value = "Session";
            HttpContext.Current.Response.Cookies.Set(SESSID);
            //===========================================
        }

        public static void SessionUserHandler(HttpContext httpContext)
        {
            //=========update session variable ==========
            var UserName = httpContext.Request.Cookies["UserName"];
            if (null == UserName)
            {
                UserName = new HttpCookie("UserName");
            }
            var sessTime = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
            // This -5 seconds for safety, if a client call ajax and then client side session
            //is not finished but whe the request comes to server, session already expires
            UserName.Expires = sessTime.AddSeconds(-5);
           // UserName.Value = httpContext.User.Identity.Name;
            HttpContext.Current.Response.Cookies.Set(UserName);
            //===========================================
        }

     

      

        public static string GetIPAddress()
        {
             return HttpContext.Current.Request.UserHostAddress;
        }

        public static void AuthorizeManually()
        {
            var httpContext = HttpContext.Current;


            var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var identity = new GenericIdentity(ticket.Name);
                string userData = ticket.UserData;
                if (userData.Contains("="))
                {
                    string[] data = userData.Split('=');

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
            }

            // AuthUtil.SessionHandler(HttpContext.Current);
            AuthUtil.SessionUserHandler(HttpContext.Current);



        }

        public static void SetLoggedInUser(UserInformationModel user)
        {
            GlobalCachingProvider.Instance.AddorUpdateItem(user.Id.ToString(CultureInfo.InvariantCulture), user);
        }
        public static UserInformationModel GetLoggedInUser(string userId)
        {
            return GlobalCachingProvider.Instance.GetItem(userId) as UserInformationModel;
        }
        public static void RemoveLoggedInUser(string userId)
        {
            GlobalCachingProvider.Instance.RemoveItem(userId);


        }
    }
}
