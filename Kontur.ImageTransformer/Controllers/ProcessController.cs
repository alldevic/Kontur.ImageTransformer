using System.Threading.Tasks;
using System.Web.Http;
using Kontur.ImageTransformer.ImageFilters;

namespace Kontur.ImageTransformer.Controllers
{
    public class ProcessController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Grayscale(int x, int y, int w, int h) =>
            await Filtering.Do(Request.Content.ReadAsStreamAsync().Result, x, y, w, h, new GrayscaleFilter());

        [HttpPost, Route("process/threshold({level:int:range(0,100)})/{x},{y},{w},{h}")]
        public async Task<IHttpActionResult> Threshold(byte level, int x, int y, int w, int h) =>
            await Filtering.Do(Request.Content.ReadAsStreamAsync().Result, x, y, w, h, new ThresholdFilter(level));

        [HttpPost]
        public async Task<IHttpActionResult> Sepia(int x, int y, int w, int h) =>
            await Filtering.Do(Request.Content.ReadAsStreamAsync().Result, x, y, w, h, new SepiaFilter());
    }
}