using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
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
        [HttpPost, Route("{trn:transform}/{crd:coords}")]
        public async Task<IHttpActionResult> Transform(RotateFlipType trn, string crd)
        {
            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                return BadRequest();
            }

            var conv = new RectangleConverter();
            var rect = (Rectangle) conv.ConvertFromString(null, CultureInfo.InvariantCulture, crd);
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

        [HttpPost, Route("grayscale/{crd:coords}")]
        public async Task<IHttpActionResult> Grayscale(string crd) =>
            await Do(crd, ImageFilters.GrayscaleFilter);

        [HttpPost, Route("threshold({level:int:range(0,100)})/{crd:coords}")]
        public async Task<IHttpActionResult> Threshold(byte level, string crd) =>
            await Do(crd, ImageFilters.ThresholdFilter, level);

        [HttpPost, Route("sepia/{crd:coords}")]
        public async Task<IHttpActionResult> Sepia(string crd) =>
            await Do(crd, ImageFilters.SepiaFilter);

        private async Task<IHttpActionResult> Do(string crd, ImageFilters.Filter filter, int level = 0)
        {
            var tracer = Request.GetConfiguration().Services.GetTraceWriter();

            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Incorrect PNG");
                return BadRequest();
            }

            var conv = new RectangleConverter();
            var rect = (Rectangle) conv.ConvertFromString(null, CultureInfo.InvariantCulture, crd);
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