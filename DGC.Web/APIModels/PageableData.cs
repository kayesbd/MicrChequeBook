using System.Collections.Generic;

namespace KBZ.Web.APIModels
{
    public class PageableData<T>

    {
        public int Total { get; set; }
        public IEnumerable<T> Data{ get; set; }
    }
}