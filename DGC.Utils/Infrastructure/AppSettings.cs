using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace KBZ.Utils.Infrastructure
{
    public class AppSettings
    {
        public static IKernel GlobalDependencyResolver;
        public static Dictionary<string, object> Settings = new Dictionary<string, object>();

     
     public static void Add(string key,object value)
        {
            Settings.Add(key,value);
        }
        public static object Get(string key,Type type)
        {
            var retObject = Convert.ChangeType(Settings.SingleOrDefault(x => x.Key == key), type);
            return retObject;
        }
        public static void Remove(string key)
        {

            Settings.Remove(key);
        }
    }
}
