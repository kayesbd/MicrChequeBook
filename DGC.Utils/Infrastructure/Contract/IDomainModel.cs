namespace KBZ.Utils.Infrastructure.Contract
{
    // Marker interface

    public interface IBaseDomainModel
    {

    }

    public interface IDomainModel : IBaseDomainModel
    {
        void SetUpdateProperties(long userId);
        void SetCreateProperties(long userId);
        void MarkDeleted(object userId);
        void SetCreateProperties();
    }
}