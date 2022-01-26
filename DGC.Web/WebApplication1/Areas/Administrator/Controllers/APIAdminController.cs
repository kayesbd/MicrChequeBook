
using KBZ.Administration.Domain.Model;
using KBZ.BLL.Security;
using KBZ.CPMS.API.Models;
using KBZ.Utils;
using KBZ.Utils.Models;

using NusPay.Utils.Infrastructure;
using NusPay.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace KBZ.CPMS.API.Areas.Administrator.Controllers
{
    public class APIAdminController : ApiController
    {
        public IUserInformationService userInformationService;
        public APIAdminController(IUserInformationService _IuserInformationService)
        {
            userInformationService = _IuserInformationService;
        }

        [System.Web.Mvc.HttpPost]
        public ResponseAPI Login(LoginViewModel model)
        {
            FileLogger.Info("Called login");
            var sessId = Guid.NewGuid().ToString();
            try
            {
                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                {
                  return  new ResponseAPI()
                    {
                        ResponseMessage = "User Name or Password can not be empty."
                   };

                }

                UserInformationModel userInformation = userInformationService.GetUser(model.UserName);
                if (userInformation != null)
                {

                    if (userInformation.Password == model.Password)
                    {
                        FileLogger.Info("Password is valid and logged in");


                        if (userInformation.IsActive == false)
                        {

                            return new ResponseAPI()
                            {
                                IsResponseSuccess = false,
                                ResponseMessage = "This user is not activated yet. Please contact with your system administrator.",
                                AccessToken = null
                            };
                        }
                     



                        if (userInformation.RoleId == null)
                        {
                            userInformation.RoleId = "ChecquePrinter";
                        }



                        // -------------------------------------------------------------


                    }



                }


                UserInformationModel mod = new UserInformationModel();
                mod.SessionID = sessId;
                userInformationService.UpdateUser(mod);
                return new ResponseAPI()
                {
                    IsResponseSuccess = true,
                    ResponseMessage = "Login Successful",
                    AccessToken = sessId
                };
            }
            catch (Exception ex)
            {

                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Login Failed",
                    AccessToken = null
                };

            }

        }






        private ResponseAPI PrintChequeBook(ChequeBookPrint model)
        {
            
            if(model.AccessToken=="" )
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Access token should not be empty.",
                    AccessToken = null
                };
            }
            if (model.AccountNumber == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Account Number should not be empty.",
                    AccessToken = null
                };
            }
            if (model.BranchPickup == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Account Number should not be empty.",
                    AccessToken = null
                };
            }
            if (model.SerialNumberStart == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Serial Number Start should not be empty.",
                    AccessToken = null
                };
            }
            if (model.SerialNumberEnd == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Serial Number End should not be empty.",
                    AccessToken = null
                };
            }

            return null;
        }
    }
}