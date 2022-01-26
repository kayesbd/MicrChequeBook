using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GITS.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]

    public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                string[] strTokens = { };
                IEnumerable<string> tokenHeaders;
                if (actionContext.Request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
                {
                    strTokens = tokenHeaders.FirstOrDefault().Split(':');
                    AntiForgery.Validate(strTokens[0], strTokens[1]);
                }
            }
            catch (System.Web.Mvc.HttpAntiForgeryException e)
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    RequestMessage = actionContext.ControllerContext.Request
                };
                return FromResult(actionContext.Response);
            }
            return continuation();
        }

        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }
    }
}
