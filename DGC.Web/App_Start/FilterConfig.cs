using GITS.Web;
using System.Web.Mvc;

namespace GITS.Web
{
    public class FilterConfig
    {
      
        internal static void RegisterHttpFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new APIAuthorizeAttribute());
            filters.Add(new GITSApiExceptionHandler());
          
        }
    }
}
