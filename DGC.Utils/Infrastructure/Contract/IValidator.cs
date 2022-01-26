using System.Collections.Generic;

namespace KBZ.Utils.Infrastructure.Contract
{
    /// <summary>
    /// Interface to use the visitor pattern to seperate validation logic into validator classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> where T: IDomainModel
    {
        bool IsValid(T entity);
        IEnumerable<string> BrokenRules(T entity);
    }
}