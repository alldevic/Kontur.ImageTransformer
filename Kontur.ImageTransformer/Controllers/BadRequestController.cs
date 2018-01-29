using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kontur.ImageTransformer.Controllers
{
    /// <summary>
    /// Customize 404 NotFound error page to 400 BadRequest 
    /// </summary>
    public class BadRequestController : ApiController
    {
        [HttpPost, AcceptVerbs("PATCH")]
        public HttpResponseMessage Handle404() =>
            new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
}