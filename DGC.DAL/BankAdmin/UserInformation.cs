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
    
    public partial class UserInformation
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenValidityPeriod { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> LastPasswordChangeDate { get; set; }
        public Nullable<int> PasswordExpirationRemainingAge { get; set; }
        public Nullable<bool> IsNotifyRequiredforPassword { get; set; }
        public Nullable<bool> IsPasswordExpired { get; set; }
        public string UserType { get; set; }
    }
}
