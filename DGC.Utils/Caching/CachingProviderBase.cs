using System;
using System.IO;
using System.Runtime.Caching;

namespace KBZ.Utils.Caching
{
    public abstract class CachingProviderBase
    {
        public CachingProviderBase()
        {
            
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            DeleteLog();
        }

        protected MemoryCache Cache = new MemoryCache("CachingProvider");

        static readonly object Padlock = new object();

        protected virtual void AddItem(string key, object value)
        {
            try
            {
                lock (Padlock)
                {
                    Cache.Add(key, value, DateTimeOffset.MaxValue);
                }
            }
            catch(Exception ex)
            {
                WriteToLog(" Error AddItem() : " +  ex.Message);
            }
        }
        protected virtual void AddNewItem(string key, object value, TimeSpan timeSpan)
        {
            var policy = new CacheItemPolicy { SlidingExpiration = timeSpan };

            lock (Padlock)
            {
                Cache.Add(new CacheItem(key, value), policy);
            }
        }
        protected virtual void AddorUpdateItem(string key, object value, TimeSpan timeSpan)
        {
            try
            {
                var policy = new CacheItemPolicy { SlidingExpiration = timeSpan };
                lock (Padlock)
                {
                    if (Cache.Contains(key)) Cache.Remove(key);
                    Cache.Add(new CacheItem(key, value), policy);
                }
            }
            catch (Exception ex)
            {

                WriteToLog(" Error AddorUpdateItem() : " + ex.Message);
            }
         
        }

        public virtual void RemoveItem(string key)
        {
            lock (Padlock)
            {
                Cache.Remove(key);
            }
        }

        protected virtual object GetItem(string key, bool remove)
        {
            lock (Padlock)
            {
                var res = Cache[key];
                

                if (res != null)
                {
                    if (remove == true)
                        Cache.Remove(key);
                }
                else
                {
                    WriteToLog("CachingProvider-GetItem: Don't contains key: " + key);
                    return null;
                   
                }

                return res;
            }
        }

        #region Error Logs

        string LogPath = AppDomain.CurrentDomain.BaseDirectory+"\\CachingLog\\";

        protected void DeleteLog()
        {
            try
            {
                
                lock (Padlock)
                {
                    string errorFileName = string.Format("{0}\\CachingProvider_Errors.txt", LogPath);
                    if (File.Exists(errorFileName))
                    {
                        System.IO.File.Delete(errorFileName);
                    }
                }
            }
            catch(Exception ex)
            {
                WriteToLog(" Error DeleteLog() : " + ex.Message);
            }
            
        }
        public static readonly object lockObject = new object();
        protected void WriteToLog(string text)
        {
            try
            {
                lock (Padlock)
                {
                    lock (lockObject)
                    {
                        
                        using (System.IO.TextWriter tw = System.IO.File.AppendText(string.Format("{0}\\CachingProvider_Errors.txt", LogPath)))
                        {
                            tw.WriteLine(text);
                            tw.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        #endregion
    }
}
