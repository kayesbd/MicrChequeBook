//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KBZ.DAL.BankAdmin
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrinterInformation
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