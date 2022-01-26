using System;
using System.Collections.Generic;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    /// <summary>
    /// A central registry of all the validators that we're using for our domain objects
    /// This could be hooked up via IoC..
    /// </summary>
    public static class Validator
    {
        private static readonly Dictionary<Type, object> _validators = new Dictionary<Type, object>();

        public static void RegisterValidatorFor<T>(Type entityType, IValidator<T> validator)
            where T : BaseDomainModel<T>
        {
            if(!(_validators.ContainsKey(entityType)))
                _validators.Add(entityType, validator);
        }

        public static IValidator<T> GetValidatorFor<T>(T entity)
            where T : BaseDomainModel<T>
        {
            object value;
            var found = _validators.TryGetValue(entity.GetType(), out value);
            if (found)
                return value as IValidator<T>;

            return null;
        }

        /// <summary>
        /// This should be hooked up through our IOC implementation so that it can be reused
        /// across the web project
        /// </summary>
        public static void RegisterValidationHandlers()
        {
           
        }
    }
}
