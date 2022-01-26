using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NusPay.Web.APIModels;
using NusPay.BLL.Common;
using NusPay.Common.Domain.Model;

namespace NusPay.Web.Areas.Administrator.Controllers
{
    public class ApiCountryController : ApiController
    {
        private ICountryService _countryServices = null;
        private readonly IStateService _stateService;

        public ApiCountryController(ICountryService countryServices, IStateService stateService)
        {
            _countryServices = countryServices;
            _stateService = stateService;
        }

        // GET api/Country
        [HttpGet]
        public IEnumerable<CountryModel> GetCountryList()
        {
            var data = _countryServices.GetAllCountries();
            return data;
        }

        //// GET api/Country/5
        //[HttpGet]
        //public CountryModel GetCountryListById(int id)
        //{
        //    CountryModel Country = _countryServices.SingleOrDefault(id);
        //    if (Country == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return Country;
        //}

        // PUT api/Country/5
        //public APIResponse PutCountry(int Id, CountryModel Country)
        //{

        //    _countryServices.Update(Country);
        //    return new APIResponse() { Success = true, Id = Country.Id, ErrorMessage = "", ModelValue = Country };
        //}

        //// POST api/Country
        //public APIResponse PostCountry(CountryModel Country)
        //{
        //    //PersonalInformation.CountryId = null;
        //    long id = _countryServices.Insert(Country);
        //    return new APIResponse() { Success = true, Id = id, ErrorMessage = "", ModelValue = Country };
        //}

        //// DELETE api/Country/5
        //public APIResponse DeleteCountry(int id)
        //{
        //    if (id > 0)
        //    {
        //        _countryServices.Delete(id);
        //    }
        //    return new APIResponse() { Success = true, Id = id, ModelValue = _countryServices.SingleOrDefault(id) };
        //}

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

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
