using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Http;
using Kendo.Mvc.UI;
using NusPay.BLL.Client;
using NusPay.BLL.Transaction;
using NusPay.Transaction.Domain.Model;
using System.Collections.Generic;
using System;
using NusPay.Utils.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Globalization;
using NusPay.Security.Domain.Model;
using NusPay.Utils;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuspay.TransactionCoreEngine.Models.ViewModels;
using NusPay.DAL.Transaction;
using NusPay.Transaction.ViewModel;
using NusPay.Web.APIModels;
using FundTransferViewModel = NusPay.Web.Areas.Client.Models.FundTransferViewModel;
using SettlementViewModel = NusPay.Web.Areas.Administrator.Models.SettlementViewModel;

namespace NusPay.Web.Areas.Transaction.Controllers
{
    public class ApiTransactionController : ApiController
    {
        private readonly ITransactionInformationService _transactionInformationService;
        private readonly UserInformationModel _loginUserInformation;

        public ApiTransactionController(ITransactionInformationService transactionInformaitonService, IBankService bankService)
        {
            _transactionInformationService = transactionInformaitonService;
            if (HttpContext.Current.Session["UserInformation"] != null)
            {
                _loginUserInformation = (UserInformationModel)HttpContext.Current.Session["UserInformation"];
            }
        }

        [HttpGet]
        public TransactionDetails GetTransactionDetails(long id)
        {
            var transactionDetails = _transactionInformationService.GetTransactionDetails(id, null);

            var returnTransactionDetails = new TransactionDetails();

            if (_loginUserInformation.UserType == UserType.Customer.ToString())
            {
                if (transactionDetails.CustomerId == _loginUserInformation.ClientId || transactionDetails.ReceiverCustomerId == _loginUserInformation.ClientId)
                {
                    returnTransactionDetails = transactionDetails;
                }
            }
            if (_loginUserInformation.UserType == UserType.Merchant.ToString())
            {
                if (transactionDetails.MerchantId == _loginUserInformation.ClientId)
                {
                    returnTransactionDetails = transactionDetails;
                }
            }
            if (_loginUserInformation.UserType == UserType.BankAdmin.ToString())
            {
                if (transactionDetails.FromBankCode == _loginUserInformation.BankCode || transactionDetails.ToBankCode == _loginUserInformation.BankCode)
                {
                    returnTransactionDetails = transactionDetails;
                }
            }
            if (_loginUserInformation.UserType == UserType.SystemAdmin.ToString())
            {
                returnTransactionDetails = transactionDetails;
            }

            return returnTransactionDetails;
        }

        [HttpPost]
        public DataSourceResult SearchTransactionInformation([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;
            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, transactionType, transactionStatus, Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, Convert.ToInt32(customerId), customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = 0 };
                return dResult;
            }
        }
        [HttpPost]
        public DataSourceResult GetTransactionsByByDcStatus([DataSourceRequest]DataSourceRequest request, string customerId, string merchantId, string virtualAccountStatus, string transactionType, string transactionStatus, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;
            var result = _transactionInformationService.GetTransactionsByByDcStatus(pageSize, pageNum, customerId, merchantId, virtualAccountStatus, transactionType, transactionStatus, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpPost]
        public DataSourceResult SearchMerchantWiseTransactionInformation([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;

            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, transactionType, transactionStatus, Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, _loginUserInformation.ClientId, customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpPost]
        public DataSourceResult CompleteTransaction([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;

            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, transactionType, TransactionStatusSale.Settled.ToString(), Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, _loginUserInformation.ClientId, customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpPost]
        public DataSourceResult DeclinedTransaction([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;

            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, transactionType, TransactionStatusSale.Declined.ToString(), Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, _loginUserInformation.ClientId, customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpPost]
        public DataSourceResult SearchFundTransferReport([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;

            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, TransactionType.FundTransfer.ToString(), transactionStatus, Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, _loginUserInformation.ClientId, customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpPost]
        public DataSourceResult InstantBillPaymentReport([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;

            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, TransactionType.IBPOnPresence.ToString(), transactionStatus, Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, _loginUserInformation.ClientId, customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpPost]
        public DataSourceResult CashOutReport([DataSourceRequest]DataSourceRequest request, string transactionId, string transactionType, string transactionStatus, string merchantId, string companyName, string companyShortName, string fromBankCode, string customerId, string customerMobileNumber, string customerEmail, string customerFirstName, string customerLastName, string fromDateTime, string toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;

            var result = _transactionInformationService.GetTransactionInformationBySearch(pageSize, pageNum, transactionId, TransactionType.CashWithdrawal.ToString(), transactionStatus, Convert.ToInt32(merchantId), companyName, companyShortName, fromBankCode, _loginUserInformation.ClientId, customerMobileNumber, customerEmail, customerFirstName, customerLastName, Convert.ToDateTime(fromDateTime), Convert.ToDateTime(toDateTime));
            if (result.Any())
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = Convert.ToInt16(result.FirstOrDefault().Total) };
                return dResult;
            }
            else
            {
                DataSourceResult dResult = new DataSourceResult() { Data = result, Total = 0 };
                return dResult;
            }
        }

        [HttpGet]
        public TransactionInformationModel GetCustomerVirtualAccountDetails(long customerId, string virtualAccountNo)
        {
            return _transactionInformationService.GetCustomerVirtualAccountDetails(customerId, virtualAccountNo);
        }

        [HttpPost]
        public List<TransactionInformationModel> GetTransactionsByDCStatus([DataSourceRequest]DataSourceRequest request, string virtualAccountStatus, DateTime? fromDateTime, DateTime? toDateTime)
        {
            int pageSize = request.PageSize;
            int pageNum = request.Page;
            var result = _transactionInformationService.GetTransactionsByDCStatus(virtualAccountStatus, fromDateTime, toDateTime, pageSize, pageNum).ToList();
            return result;
        }

        [HttpGet]

        public List<string> GetTransactionTypeList()
        {
            return _transactionInformationService.GetTransactionTypeList();
        }
        [HttpGet]
        public List<string> GetVirtualAccountStatusList()
        {
            return _transactionInformationService.GetVirtualAccountStatusList();
        }

        [HttpGet]
        public List<string> GetTransactionStatusList(TransactionType transactionType)
        {

            return _transactionInformationService.GetTransactionStatusList(transactionType);
        }

        [HttpGet]
        public List<TransactionInformationReportModel> GetTransactionInformation()
        {
            var customerTransaction = _transactionInformationService.GetTransactionInformation(null);
            return customerTransaction;
        }

        [AllowAnonymous]
        [HttpPost]
        [NusPayHttpActivityLogger("Process Fund Transfer")]
        public HttpResponseMessage ProcessFundTransfer(FundTransferViewModel fundTransfer)
        {
            var webClient = new WebClient();
            var formData = new NameValueCollection();

            CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
            if (principal == null)
            {
                if (HttpContext.Current.User != null)
                {
                    principal = HttpContext.Current.User as CustomUserPrincipal;
                }
            }

            formData["FundTransferAmount"] = fundTransfer.FundTransferAmount.ToString(CultureInfo.InvariantCulture);
            formData["ReceiverCustomerMobileNumber"] = fundTransfer.ReceiverCustomerMobileNumber;
            formData["SenderCustomerPIN"] = fundTransfer.SenderCustomerPIN;
            if (principal != null) formData["SenderCustomerMobileNumber"] = principal.MobileNumber.ToString();

            var requestUrl = ConfigurationManager.AppSettings["NuspayAPI"] + "/api/Payment/FundTransfer";
            try
            {
                var responseBytes = webClient.UploadValues(requestUrl, "POST", formData);
                if (responseBytes != null)
                {
                    var response = Encoding.UTF8.GetString(responseBytes);
                    var result = JsonConvert.DeserializeObject(response);
                    var obj = JObject.Parse(response);
                    var isOperationSuccessful = (bool)obj.SelectToken("IsOperationSuccessful");
                    if (isOperationSuccessful)
                    {
                        var transactionId = (string)obj.SelectToken("TransactionId");
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, (string)obj.SelectToken("Message"));
                }
            }
            catch (WebException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
        }

        [AllowAnonymous]
        [HttpPost]
        [NusPayHttpActivityLogger("Process Fund Transfer Virtual Account")]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage ProcessFundTransferVirtualAccount(FundTransferViewModel fundTransfer)
        {
            var webClient = new WebClient();
            var formData = new NameValueCollection
            {
                ["TransactionId"] = fundTransfer.TransactionId,
                ["VirtualAccountNumber"] = fundTransfer.VirtualAccountNumber
            };

            var requestUrl = ConfigurationManager.AppSettings["NuspayAPI"] + "/api/Payment/FundTransferVASubmissionFromSender";

            var responseBytes = webClient.UploadValues(requestUrl, "POST", formData);
            if (responseBytes != null)
            {
                var response = Encoding.UTF8.GetString(responseBytes);
                try
                {
                    var result = JsonConvert.DeserializeObject(response);
                    var obj = JObject.Parse(response);
                    var isOperationSuccessful = (bool)obj.SelectToken("IsOperationSuccessful");
                    if (isOperationSuccessful)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, (string)obj.SelectToken("Message"));
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
        }

        [AllowAnonymous]
        [HttpPost]
        [NusPayHttpActivityLogger("Process Fund Transfer Virtual Account")]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage FundTransferOTPSubmission(FundTransferViewModel fundTransfer)
        {
            var webClient = new WebClient();
            var formData = new NameValueCollection
            {
                ["TransactionId"] = fundTransfer.TransactionId,
                ["OTP"] = fundTransfer.OTP
            };

            var requestUrl = ConfigurationManager.AppSettings["NuspayAPI"] + "/api/Payment/FundTransferOTPSubmission";

            var responseBytes = webClient.UploadValues(requestUrl, "POST", formData);
            if (responseBytes != null)
            {
                var response = Encoding.UTF8.GetString(responseBytes);
                try
                {
                    var result = JsonConvert.DeserializeObject(response);
                    var obj = JObject.Parse(response);
                    var isOperationSuccessful = (bool)obj.SelectToken("IsOperationSuccessful");
                    if (isOperationSuccessful)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, (string)obj.SelectToken("Message"));
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
        }

        //[HttpPost]
        //public List<OffUsReportSearchModel> TransactionReportOwnCustomre(OffUsReportSearchModel settelmentReport)
        //{
        //    long personId = _loginUserInformation.PersonId;
        //    string bankCode = _bankService.GetBankCode(personId);
        //    settelmentReport.FromBankCode = bankCode;
        //    settelmentReport.TransactionStatusId = 1;
        //    var transactionReporOwnCustomreAndSander = _transactionInformaitonService.GetTransactionReportOwnCustomreAndSander(settelmentReport).Where(r => r.ReportType == "Own Customer").ToList();

        //    foreach (var transactionRepor in transactionReporOwnCustomreAndSander)
        //    {
        //        if (transactionRepor.DateCreated != null)
        //        {
        //            transactionRepor.StrDateCreated = Utility.GetUserDatetime((DateTime)transactionRepor.DateCreated, _loginUserInformation.UTCTimeId, _loginUserInformation.CountryShortCode);
        //        }
        //    }
        //    return transactionReporOwnCustomreAndSander;
        //}

        //[HttpPost]
        //public List<OffUsReportSearchModel> TransactionReportOwnSender(OffUsReportSearchModel settelmentReport)
        //{
        //    long personId = _loginUserInformation.PersonId;
        //    string bankCode = _bankService.GetBankCode(personId);
        //    settelmentReport.FromBankCode = bankCode;
        //    settelmentReport.TransactionStatusId = 1;
        //    var transactionReporOwnCustomreAndSander = _transactionInformaitonService.GetTransactionReportOwnCustomreAndSander(settelmentReport).Where(r => r.ReportType == "Own Sender").ToList();


        //    foreach (var transactionRepor in transactionReporOwnCustomreAndSander)
        //    {
        //        if (transactionRepor.DateCreated != null)
        //        {
        //            transactionRepor.StrDateCreated = Utility.GetUserDatetime((DateTime)transactionRepor.DateCreated, _loginUserInformation.UTCTimeId, _loginUserInformation.CountryShortCode);
        //        }
        //    }
        //    return transactionReporOwnCustomreAndSander;
        //}

        //[HttpPost]
        //public List<OffUsReportSearchModel> TransactionReportOwnMerchant(OffUsReportSearchModel settelmentReport)
        //{
        //    long personId = _loginUserInformation.PersonId;
        //    string bankCode = _bankService.GetBankCode(personId);
        //    settelmentReport.FromBankCode = bankCode;
        //    settelmentReport.TransactionStatusId = 1;
        //    var transactionReporOwnCustomreAndReceiver = _transactionInformaitonService.GetTransactionReportOwnMerchantAndReceiver(settelmentReport).Where(r => r.ReportType == "Own Merchant").ToList();

        //    foreach (var transactionRepor in transactionReporOwnCustomreAndReceiver)
        //    {
        //        if (transactionRepor.DateCreated != null)
        //        {
        //            transactionRepor.StrDateCreated = Utility.GetUserDatetime((DateTime)transactionRepor.DateCreated, _loginUserInformation.UTCTimeId, _loginUserInformation.CountryShortCode);
        //        }
        //    }
        //    return transactionReporOwnCustomreAndReceiver;
        //}

        //[HttpPost]
        //public List<OffUsReportSearchModel> TransactionReportOwnReceiver(OffUsReportSearchModel settelmentReport)
        //{
        //    long personId = _loginUserInformation.PersonId;
        //    string bankCode = _bankService.GetBankCode(personId);
        //    settelmentReport.FromBankCode = bankCode;
        //    settelmentReport.TransactionStatusId = 1;
        //    var transactionReporOwnCustomreAndSander = _transactionInformaitonService.GetTransactionReportOwnMerchantAndReceiver(settelmentReport).Where(r => r.ReportType == "Own Receiver").ToList();

        //    foreach (var transactionRepor in transactionReporOwnCustomreAndSander)
        //    {
        //        if (transactionRepor.DateCreated != null)
        //        {
        //            transactionRepor.StrDateCreated = Utility.GetUserDatetime((DateTime)transactionRepor.DateCreated, _loginUserInformation.UTCTimeId, _loginUserInformation.CountryShortCode);
        //        }
        //    }
        //    return transactionReporOwnCustomreAndSander;
        //}

        [HttpPost]
        public HttpResponseMessage ProcessSettlement(SettlementViewModel viewModel)
        {
            var webClient = new WebClient();
            var formData = new NameValueCollection();

            var requestUrl = ConfigurationManager.AppSettings["NuspayAPI"] + "/api/Payment/MerchantSettlement";

            formData.Add("FileSend", viewModel.FileSend.ToString());
            formData.Add("FileReceive", viewModel.FileReceive.ToString());
            var responseBytes = webClient.UploadValues(requestUrl, "POST", formData);
            if (responseBytes != null)
            {
                var response = Encoding.UTF8.GetString(responseBytes);
                try
                {
                    var result = JsonConvert.DeserializeObject(response);
                    var obj = JObject.Parse(response);
                    var isOperationSuccessful = (bool)obj.SelectToken("IsOperationSuccessful");
                    var message = (string)obj.SelectToken("Message");
                    if (isOperationSuccessful)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
        }

        [HttpPost]
        public FundTransferFeeInformationViewModel GetFundTransferFeeInformation(FundTransferViewModel fundTransferInformation)
        {
            long customerId = Convert.ToInt64(_loginUserInformation.ClientId);
            return _transactionInformationService.GetFundTransferFeeInformation(customerId, fundTransferInformation.FundTransferAmount, fundTransferInformation.ReceiverCustomerMobileNumber);
        }

        [HttpGet]
        public List<TransactionSettlementInformationViewModel> GetSettlementBatchDetails(string id)
        {
            var result = _transactionInformationService.GetSettlementBatchDetails(id);
            return result;

            // return new List<TransactionSettlementInformationViewModel>();
        }

        [HttpPost]
        public List<TransactionBatchInformationViewModel> GetsettelmentSummaryReportByBank(TransactionBatchInformationViewModel viewModel)
        {
            var result = _transactionInformationService.GetsettelmentSummaryReportByBank(viewModel.AcquirerBankCode, viewModel.BatchCreatedDate);
            return result;
        }

        [HttpGet]
        public List<string> GetSettelmentDate()
        {
            var settelmentDate = _transactionInformationService.GetSettelmentDate();
            return settelmentDate;
        }
        [HttpPost]
        public List<TransactionReportForAnonymousCustomerModel> GetTransactionReportForAnonymousCustomer(TransactionReportForAnonymousCustomerViewModel viewModel)
        {
            return _transactionInformationService.GetTransactionReportForAnonymousCustomer(viewModel);
        }
        [HttpPost]
        public List<TransactionReportForAnonymousCustomerModel> GetDCGenerationAndUsesInformations(TransactionInformationModel viewModel)
        {
            return _transactionInformationService.GetDCGenerationAndUsesInformations(viewModel.TransactionId);
        }

        [HttpPost]
        public HttpResponseMessage CashDepositWithdraw(CashDepositViewModel viewModel)
        {
            if (_loginUserInformation.UserType == UserType.InstituteAdmin.ToString())
            {
                viewModel.UserSecurityKey = _loginUserInformation.UserSecurityKey;
                viewModel.UserName = _loginUserInformation.UserName;
            }

            var webClient = new WebClient();
            var formData = new NameValueCollection();

            var requestUrl = ConfigurationManager.AppSettings["NuspayAPI"] + "/api/Payment/CashDepositAnonymousMerchant";

            formData.Add("UserName", viewModel.UserName.ToString());
            formData.Add("UserSecurityKey", viewModel.UserSecurityKey.ToString());
            formData.Add("ReceiverMobileNumber", viewModel.ReceiverMobileNumber.ToString());
            formData.Add("CashAmount", viewModel.CashAmount.ToString());

            var responseBytes = webClient.UploadValues(requestUrl, "POST", formData);
            if (responseBytes != null)
            {
                var response = Encoding.UTF8.GetString(responseBytes);
                try
                {
                    var result = JsonConvert.DeserializeObject(response);
                    var obj = JObject.Parse(response);
                    var isOperationSuccessful = (bool)obj.SelectToken("IsOperationSuccessful");
                    var message = (string)obj.SelectToken("Message");
                    if (isOperationSuccessful)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "false");

        }


    }
}
