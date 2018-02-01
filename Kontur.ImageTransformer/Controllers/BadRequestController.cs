using System.Threading.Tasks;
using System.Web.Http;

namespace Kontur.ImageTransformer.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Customize 404 NotFound error page to 400 BadRequest 
    /// </summary>
    public class BadRequestController : ApiController
    {
        [HttpPost, AcceptVerbs("PATCH")]
        public async Task<IHttpActionResult> Handle404() =>
            await Task.FromResult(BadRequest());
    }
}