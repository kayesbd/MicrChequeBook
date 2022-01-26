using KBZ.DAL.BankAdmin;
using KBZ.Utils.Infrastructure;
using System;
using System.Collections.Generic;

namespace KBZ.Administration.Domain.Model
{
    public class BranchOfBankModel 
    {

        public long Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string BranchLocation { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> PrinterId { get; set; }
        public Nullable<System.DateTime> PrintStartDate { get; set; }
        public Nullable<System.DateTime> LastPrintingDateTime { get; set; }
        public Nullable<long> TotalNumberOfLeafsPrinted { get; set; }
        public Nullable<long> TotalNumberOfBooks { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public virtual PrinterInformation PrinterInformation { get; }
    }

  }
