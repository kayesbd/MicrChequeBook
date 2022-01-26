using KBZ.Utils.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBZ.CPMS.API.Models
{
    public class ChequeBookPrint: BaseDomainModel<ChequeBookPrint>
    {

        public string AccountNumber { get; set; }
        public string BranchPickup { get; set; }
        public string SerialNumberStart { get; set; }
        public string SerialNumberEnd { get; set; }
        public string AccessToken { get; set; }
        public string ChequeType { get; set; }
        public int NumberOfLeaf { get; set; }
    }
}