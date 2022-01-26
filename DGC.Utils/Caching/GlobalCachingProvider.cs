using System;
using Util.CachingObjects;

namespace KBZ.Utils.Caching
{
    public class GlobalCachingProvider : CachingProviderBase, IGlobalCachingProvider
    {
        #region Singelton (inheriting enabled)

        protected GlobalCachingProvider()
        {

        }

        public static GlobalCachingProvider Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly GlobalCachingProvider instance = new GlobalCachingProvider();
        }

        #endregion

        #region ICachingProvider

        public virtual new void AddItem(string key, object value)
        {
            base.AddItem(key, value);
        }

        public virtual new void AddorUpdateItem(string key, object value, TimeSpan timeSpan)
        {
            // Retain time set to 2 day incase timespan not set.
            if (!(timeSpan.TotalMilliseconds > 0))
                timeSpan = new TimeSpan(2, 0, 0, 0);

            base.AddorUpdateItem(key, value, timeSpan);
        }

        public virtual new void AddItem(string key, object value, TimeSpan timeSpan)
        {
            // Retain time set to 2 day incase of timespan not set.
            if (!(timeSpan.TotalMilliseconds > 0))
                timeSpan = new TimeSpan(2, 0, 0, 0);

            base.AddNewItem(key, value, timeSpan);
        }

        public virtual object GetItem(string key)
        {
            return base.GetItem(key, false);
            //Remove default is false because it's Global Cache!
        }

        public virtual new object GetItem(string key, bool remove)
        {
            return base.GetItem(key, remove);
        }

        #endregion

        public void AddorUpdateItem(string key, Object value)
        {
            if (GlobalCachingProvider.Instance.GetItem(key) == null)
            {
                GlobalCachingProvider.Instance.AddItem(key, value);
            }
            else
            {

                GlobalCachingProvider.Instance.RemoveItem(key);
                GlobalCachingProvider.Instance.AddItem(key,value);
            }
        }
    }
}
