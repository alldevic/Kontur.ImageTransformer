using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace Kontur.ImageTransformer.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Customise 404 NotFound error page to 400 BadRequest 
    /// </summary>
    public class BadRequestController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Handle404()
        {
            Request.GetConfiguration().Services.GetTraceWriter().Info(Request,
                ControllerContext.ControllerDescriptor.ControllerType.FullName, "Handle 404");

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}