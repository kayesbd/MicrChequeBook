
using KBZ.Administration.Domain.Model;
using KBZ.BLL.Security;
using KBZ.CPMS.API.Models;
using KBZ.Utils.Models;

using KBZ.Utils.Infrastructure;


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
using DGC.BLL;
using DGC.BLL.Admin.Model;

namespace KBZ.CPMS.API.Areas.Administrator.Controllers
{
    public class APIAdminController : ApiController
    {
        public IUserInformationService userInformationService;
        public PrinterInformationService printInformationService;
        public APIAdminController(IUserInformationService _IuserInformationService)
        {
            userInformationService = _IuserInformationService;
            printInformationService = new PrinterInformationService();

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






        private ResponseAPI PrintChequeBook(PrintRequesition model)
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
            if (model.PickUpBranchCode == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Account Number can not be empty.",
                    AccessToken = null
                };
            }
            if (model.SerialNumberStart == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Serial Number Start can not be empty.",
                    AccessToken = null
                };
            }
         
            if (model.NumberOfLeaf == 0)
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "Number of Leaf can not be empty.",
                    AccessToken = null
                };
            }
            if (model.ChequeBookType == "")
            {
                return new ResponseAPI()
                {
                    IsResponseSuccess = false,
                    ResponseMessage = "ChequeType of Leaf can not be empty.",
                    AccessToken = null
                };
            }
            FileLogger.Info("Start Routing Algorithm");

            printInformationService.SaveRequistion(model);

            return null;
        }
    }
}