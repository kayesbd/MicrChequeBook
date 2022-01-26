using System.Security.Principal;

namespace KBZ.Utils.Infrastructure
{
    public class CustomUserPrincipal : GenericPrincipal
    {
        public CustomUserPrincipal(IIdentity identity, long Id, string email, string role, string fullName,
            long personId, long organizationId, string exchangeUID, string exchangePWD, string exchangeDomain, long clientId, string userType, string utcTimeId,string mobileNumber)
            : base(identity, null)
        {
          
            this.Id = Id;
            Email = email;
            FullName = fullName;
            Role = role;
            PersonId = personId;
            OrganizationId = organizationId;
            ExchangeUID = exchangeUID;
            ExchangePwd = exchangePWD;
            ExchangeDomain = exchangeDomain;
            ClientId = clientId;
            UserType = userType;
            UTCTimeId = utcTimeId;
            MobileNumber = mobileNumber;
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public long PersonId { get; set; }
        public long OrganizationId { get; set; }
        public string ExchangeUID { get; set; }
        public string ExchangePwd { get; set; }
        public string ExchangeDomain { get; set; }
        public long ClientId { get; set; }
        public string UserType { get; set; }
        public string UTCTimeId { get; set; }
    }
}
