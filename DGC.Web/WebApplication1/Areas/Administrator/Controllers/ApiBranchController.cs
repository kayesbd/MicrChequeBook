using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KBZ.CPMS.API.Areas.Administrator.Controllers
{
    public class ApiBranchController : ApiController
    {
        // GET: api/ApiBranch
        public IList<string> GetAllData()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiBranch/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiBranch
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiBranch/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiBranch/5
        public void Delete(int id)
        {
        }
    }
}
