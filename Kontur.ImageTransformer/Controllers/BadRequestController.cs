using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kontur.ImageTransformer.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Customise 404 NotFound error page to 400 BadRequest 
    /// </summary>
    public class BadRequestController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Handle404() => new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
}