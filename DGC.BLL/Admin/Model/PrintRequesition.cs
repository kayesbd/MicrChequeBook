using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.BLL.Admin.Model
{
    public class PrintRequesition
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public string SerialNoOfCheqStart { get; set; }
        public string SerialNoOfCheqEnd { get; set; }
        public string ChequeBookType { get; set; }
        public Nullable<int> NoOfLeaf { get; set; }
        public String PickUpBranchCode { get; set; }
        public string RequsitionSlipSerialNumber { get; set; }
        public string NRCPerson { get; set; }
        public Nullable<long> PickupBranchId { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string DeliveredBy { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ChequeBranch { get; set; }
        public Nullable<int> ChequeOrderBy { get; set; }
        public Nullable<int> ChequePrintedCommandBy { get; set; }
        public Nullable<System.DateTime> OrderDateTime { get; set; }
        public Nullable<System.DateTime> PrintDateTime { get; set; }
        public string AccessToken { get; set; }
      
        public string CustomerName { get; set; }
     
        public string Cheque_location_code_RequestBranch_Code { get; set; }
        public string Location { get; set; }
        public string RequestBranchCode { get; set; }
    
        public string SerialNumberStart { get; set; }
 
        public Nullable<int> NumberOfLeaf { get; set; }
        public Nullable<long> PrinterId { get; set; }

      
      

    }
}
