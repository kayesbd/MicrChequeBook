using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    public class NonAuditableDomainModel<T> : IBaseDomainModel where T : class
    {
        public virtual long Id { get; set; }

    }
}
