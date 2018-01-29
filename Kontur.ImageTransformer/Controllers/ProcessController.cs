using System.Web.Http;
using System.Web.Http.Results;

namespace Kontur.ImageTransformer.Controllers
{
    public class ProcessController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Grayscale(int x, int y, int w, int h) =>
            new OkResult(Request);

        [HttpPost, Route("process/threshold({level:int:range(0,100)})/{x},{y},{w},{h}")]
        public IHttpActionResult Threshold(byte level, int x, int y, int w, int h) =>
            new OkResult(Request);

        [HttpPost]
        public IHttpActionResult Sepia(int x, int y, int w, int h) =>
            new OkResult(Request);
    }
}