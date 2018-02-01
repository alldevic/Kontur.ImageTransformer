using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Kontur.ImageTransformer.Selectors
{
    /// <summary>
    /// Overload for response 400 on bad {controller}, like "proces" instead "process" 
    /// </summary>
    public class Http404DefaultSelector : DefaultHttpControllerSelector
    {
        public Http404DefaultSelector(HttpConfiguration configuration) : base(configuration)
        {
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor;
            try
            {
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound)
                {
                    throw;
                }

                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "BadRequest";
                routeValues["action"] = "Handle404";
                decriptor = base.SelectController(request);
            }

            return decriptor;
        }
    }
}