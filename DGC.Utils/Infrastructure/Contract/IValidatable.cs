using System.Collections.Generic;

namespace KBZ.Utils.Infrastructure.Contract
{
    public interface IValidatable
    {
        bool Validate(out IEnumerable<string> brokenRules);
    }
}