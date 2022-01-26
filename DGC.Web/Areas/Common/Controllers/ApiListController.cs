using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NusPay.BLL.Client;
using NusPay.BLL.Common;
using NusPay.BLL.Security;
using NusPay.Client.Domain.Model;
using NusPay.Common.Domain.Model;
using NusPay.Security.Domain.Model;
using NusPay.Utils.Infrastructure;
using NusPay.BLL.Transaction;
using NusPay.Transaction.Domain.Model;

namespace NusPay.Web.Areas.Common.Controllers
{
    public class ApiListController : ApiController
    {
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly IUserTypeService _userTypeService;
        private readonly IAccountTypeService _accountTypeService;
        private readonly ICurrencyService _currencyInfoService;
        private readonly IBusinessCategoryService _businessCategoryService;
        private readonly IMerchantCategoryService _merchantCatetoryService;
        private readonly IBankService _bankService;
        private readonly IBranchOfBankService _branchOfBankService;
        private readonly IBankApprovalStatusService _bankApprovalStatusService;
        private readonly IPasswordProcessStatusService _passwordProcessStatusService;
        private readonly IUserInformationService _userInformationService;
        private readonly IDisputeStatusService _disputeStatusService;
        private readonly IMerchantStatusService _merchantStatusService;
        private readonly ICustomerStatusService _customerStatusService;
        private readonly IMerchantService _merchantService;
        private readonly IFundReceiverProfileService _fundReceiverProfileService;
        private readonly IInstitutionService _institutionService;


        public ApiListController(
            ICountryService countryService, IStateService stateService, IUserTypeService userTypeService,
            IAccountTypeService accountTypeService, ICurrencyService currencyService,
            IDisputeStatusService disputeStatusService, IBusinessCategoryService businessCategoryService,
            IMerchantCategoryService merchantCategoryService, IBankService bankService,
            IBranchOfBankService branchOfBankService, IBankApprovalStatusService bankApprovalStatusService,
            IPasswordProcessStatusService passwordProcessStatusService, IUserInformationService userInformationService,
            IMerchantStatusService merchantStatusService, ICustomerStatusService customerStatusService, 
            IMerchantService merchantService, IFundReceiverProfileService fundReceiverProfileService,
            IInstitutionService institutionService)

        {
            _countryService = countryService;
            _stateService = stateService;
            _userTypeService = userTypeService;
            _accountTypeService = accountTypeService;
            _currencyInfoService = currencyService;
            _businessCategoryService = businessCategoryService;
            _merchantCatetoryService = merchantCategoryService;
            _bankService = bankService;
            _branchOfBankService = branchOfBankService;
            _bankApprovalStatusService = bankApprovalStatusService;
            _passwordProcessStatusService = passwordProcessStatusService;
            _userInformationService = userInformationService;
            _disputeStatusService = disputeStatusService;
            _customerStatusService = customerStatusService;
            _merchantStatusService = merchantStatusService;
            _merchantService = merchantService;
            _fundReceiverProfileService = fundReceiverProfileService;
            _institutionService = institutionService;
        }

        [HttpGet]
        public IEnumerable<TimeZoneInfo> TimeZoneList()
        {
            var timeZoneList = TimeZoneInfo.GetSystemTimeZones().Where(t => t.Id == "Bangladesh Standard Time");

            if (timeZoneList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return timeZoneList;
        }

        [HttpGet]
        public IEnumerable<CountryModel> CountryList()
        {
            var data = _countryService.GetAllCountries();
            return data;
        }

        [HttpGet]
        public IEnumerable<StateModel> StateList(int id)
        {
            var stateList = _stateService.GetStateList(id);
            if (stateList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return stateList;
        }

        [HttpGet]
        public IEnumerable<string> UserTypeList()
        {
            return _userTypeService.GetAllUsertypes();
        }

        [HttpGet]
        public IEnumerable<string> AccountTypeList()
        {
            return _accountTypeService.GetAllAccountTypes();
        }

        [HttpGet]
        public IEnumerable<CurrencyModel> CurrencyList()
        {
            return _currencyInfoService.GetCurrencyList();
        }

        [HttpGet]
        public IEnumerable<BusinessCategoryModel> BusinessCategoryList()
        {
            return _businessCategoryService.GetBusinessCategoryList();
        }

        [HttpGet]
        public IEnumerable<MerchantCategoryModel> MerchantCategoryList()
        {
            return _merchantCatetoryService.GetMerchantCategoryList();
        }

        [HttpGet]
        public IEnumerable<MerchantCategoryModel> MerchantCategoryList(int businessCategoryId)
        {
            return _merchantCatetoryService.GetMerchantCategoryList(businessCategoryId);
        }

        [HttpGet]
        public IEnumerable<BankModel> BankList()
        {
            var bankList = _bankService.GetBankList();
            if (bankList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return bankList;
        }

        [HttpGet]
        public IEnumerable<BankModel> BankListByUserType()
        {
            var userPrincipal = System.Threading.Thread.CurrentPrincipal as CustomUserPrincipal;
            if (userPrincipal != null && userPrincipal.UserType == UserType.BankAdmin.ToString())
            {
                var bankListForBankAdmin = _bankService.GetBankListForBankAdmin();
                if (bankListForBankAdmin == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return bankListForBankAdmin;
            }
            var bankList = _bankService.GetBankList();
            if (bankList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return bankList;
        }

        [HttpGet]
        public IEnumerable<BranchOfBankModel> BranchList(string bankCode)
        {
            var branchList = _branchOfBankService.GetBranchList(bankCode);
            if (branchList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return branchList;
        }

        [HttpGet]
        public IEnumerable<string> BankApprovalStatusList()
        {
            return _bankApprovalStatusService.GetAllBankApprovalStatus();
        }

        [HttpGet]
        public IEnumerable<string> MerchantStatusList()
        {
            return _merchantStatusService.GetAllMerchantStatus();
        }

        [HttpGet]
        public IEnumerable<MerchantModel> MerchantList()
        {
            return _merchantService.GetAllMerchant();
        }

        [HttpGet]
        public IEnumerable<FundReceiverProfileModel> ReceiverProfileList()
        {
            var userPrincipal = System.Threading.Thread.CurrentPrincipal as CustomUserPrincipal;
            if (userPrincipal != null) return _fundReceiverProfileService.GetAllReceiverProfile(userPrincipal.ClientId).Where(x => x.IsDeleted == false);
            return null;
        }

        [HttpGet]
        public IEnumerable<string> CustomerStatusList()
        {
            return _customerStatusService.GetAllCustomerStatus();
        }

        [HttpGet]
        public IEnumerable<string> PasswordProcessStatusList()
        {
            return _passwordProcessStatusService.GetAllPasswordProcessStatus();
        }

        [HttpGet]
        public PasswordQuestionAndAnswerModel PasswordQuestionAndAnswer()
        {
            var passwordQuestionAndAnswer = _userInformationService.GetThreeQuestionsAndAnswersForRegistration();
            if (passwordQuestionAndAnswer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return passwordQuestionAndAnswer;
        }

        [HttpGet]
        public IEnumerable<string> DisputeStatusList()
        {
            return _disputeStatusService.GetAllDisputeStatus();
        }

        [HttpGet]
        public IEnumerable<InstitutionModel> InstitutionList()
        {
            var institutionList = _institutionService.GetAllInstitution();
            if (institutionList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return institutionList;
        }
    }
}
