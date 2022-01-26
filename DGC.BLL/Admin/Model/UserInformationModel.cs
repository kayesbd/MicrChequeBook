using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZ.Administration.Domain.Model
{
    public class UserInformationModel
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
        public DateTime AccessTokenValidityPeriod { get; set; }
        public bool IsActive { get; set; }
        public string IsDeleted { get; set; }
        public string DateCreated1 { get; set; }
        public string ModifiedBy { get; set; }
        public string RoleId { get; set; }
        public string SessionID { get; set; }
        public Nullable<System.DateTime> LastPasswordChangeDate { get; set; }
        public Nullable<int> PasswordExpirationRemainingAge { get; set; }
        public Nullable<bool> IsNotifyRequiredforPassword { get; set; }
        public Nullable<bool> IsPasswordExpired { get; set; }
        public string UserType { get; set; }
    }
}
