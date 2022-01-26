namespace Util.CachingObjects
{
    public interface IGlobalCachingProvider
    {
        void AddItem(string key, object value);

        object GetItem(string key);
    }
}
