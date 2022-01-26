using KBZ.DAL.BankAdmin;
using NusPay.Utils.Infrastructure;
using System;
using System.Collections.Generic;

namespace KBZ.Administration.Domain.Model
{
    public class PrinterInformationModel 
    {  
        public int Id { get; set; }
        public string PrinterNumber { get; set; }
        public string PrinterLocation { get; set; }
        public Nullable<System.DateTime> PrinterSetupDate { get; set; }
        public Nullable<System.DateTime> PrinterStartDate { get; set; }
        public Nullable<System.DateTime> LastPrintingDateTime { get; set; }
        public Nullable<long> TotalNumberOfLeafsPrinted { get; set; }
        public Nullable<long> TotalNumberOfBooks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }



    }

  
}
