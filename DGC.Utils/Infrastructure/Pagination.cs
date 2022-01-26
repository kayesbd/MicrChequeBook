using System.Collections.Generic;

namespace KBZ.Utils.Infrastructure
{
    public class Pagination<T>
    {
        public long total { get; set; }
        public List<T> rows { get; set; }
    }
}
