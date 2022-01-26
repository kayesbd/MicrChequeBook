namespace KBZ.Utils.Infrastructure.Contract
{
    public interface IDataModel
    {
        void SetUpdateProperties(long userId);
        void SetCreateProperties(long userId);
    }
}