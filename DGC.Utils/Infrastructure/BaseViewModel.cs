using System;

namespace KBZ.Utils.Infrastructure
{
    public class BaseViewModel : NonAuditableViewModel
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateModified { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public long ModifiedBy { get; set; }
        public long CreatedBy { get; set; }
        public long OrganizationId { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
    }
}
