using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using Kontur.ImageTransformer.Extensions;
using Kontur.ImageTransformer.Results;

namespace Kontur.ImageTransformer.Controllers
{
    [OverrideAuthentication, OverrideAuthorization, AllowAnonymous]
    [RoutePrefix("process")]
    public class ProcessController : ApiController
    {
        [HttpPost, Route("{trn:transform}/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<IHttpActionResult> Transform(RotateFlipType trn, int x, int y, int w, int h)
        {
            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                return BadRequest();
            }

            var rect = new Rectangle(x, y, w, h);
            rect = rect.Normalise().RotateFlip(trn, img.Width, img.Height);
            rect = Rectangle.Intersect(rect, new Rectangle(0, 0, img.Width, img.Height));

            if (rect.IsEmpty || rect.Width == 0 || rect.Height == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            img = img.Clone(rect, img.PixelFormat);
            img.RotateFlip(trn);
            return await Task.FromResult(new OkResult(img));
        }

        [HttpPost, Route("grayscale/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<IHttpActionResult> Grayscale(int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.GrayscaleFilter);

        [HttpPost, Route("threshold({level:int:range(0,100)})/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<IHttpActionResult> Threshold(byte level, int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.ThresholdFilter, level);

        [HttpPost, Route("sepia/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<IHttpActionResult> Sepia(int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.SepiaFilter);

        private async Task<IHttpActionResult> Do(int x, int y, int w, int h, ImageFilters.Filter filter, int level = 0)
        {
            var tracer = Request.GetConfiguration().Services.GetTraceWriter();

            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Incorrect PNG");
                return BadRequest();
            }

            var rect = new Rectangle(x, y, w, h);
            var plot = Rectangle.Intersect(rect.Normalise(), new Rectangle(0, 0, img.Width, img.Height));
            if (plot.IsEmpty || plot.Width == 0 || plot.Height == 0)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Empty rectangle");
                return StatusCode(HttpStatusCode.NoContent);
            }

            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Filter begin");
            var bytes = plot.Width * plot.Height;
            var argbValues = img.ToArray(plot);

            var byteLevel = (byte) (255 * level / 100);
            for (var i = 0; i < bytes; i++)
            {
                argbValues[i] = filter((uint) argbValues[i], byteLevel);
            }

            img.Dispose();
            img = argbValues.ToBitmap(plot.Width, plot.Height);
            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Filter end");

            return await Task.FromResult(new OkResult(img));
        }
    }
}