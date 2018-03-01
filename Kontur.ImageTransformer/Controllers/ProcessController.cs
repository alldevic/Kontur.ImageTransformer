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
    /// <inheritdoc />
    /// <summary>
    /// POST /process/filter/coords
    /// POST /process/transform/coords
    /// </summary>
    [OverrideAuthentication, OverrideAuthorization, AllowAnonymous]
    [RoutePrefix("process")]
    public class ProcessController : ApiController
    {
        [HttpPost, Route("{trn:transform}/{crd:coords}")]
        public async Task<IHttpActionResult> Transform(RotateFlipType trn, string crd)
        {
            var tracer = Request.GetConfiguration().Services.GetTraceWriter();

            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Incorrect PNG");
                return BadRequest();
            }

            var rect = (Rectangle) (new RectangleConverter().ConvertFromInvariantString(crd) ?? Rectangle.Empty);
            rect = rect.Normalise().RotateFlip(trn, img.Width, img.Height);
            rect = Rectangle.Intersect(rect, new Rectangle(0, 0, img.Width, img.Height));

            if (rect.IsEmpty || rect.Width == 0 || rect.Height == 0)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Empty rectangle");
                return StatusCode(HttpStatusCode.NoContent);
            }

            img = img.Clone(rect, img.PixelFormat);
            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Cropped");
            img.RotateFlip(trn);
            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Rotated");

            return await Task.FromResult(new OkResult(img, Request.Content.Headers.ContentType.MediaType));
        }

        [HttpPost, Route("{flt:filter}/{crd:coords}")]
        public async Task<IHttpActionResult> Filter(string flt, string crd)
        {
            var tracer = Request.GetConfiguration().Services.GetTraceWriter();

            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Incorrect PNG");
                return BadRequest();
            }

            var rect = (Rectangle) (new RectangleConverter().ConvertFromInvariantString(crd) ?? Rectangle.Empty);
            rect = Rectangle.Intersect(rect.Normalise(), new Rectangle(0, 0, img.Width, img.Height));
            if (rect.IsEmpty || rect.Width == 0 || rect.Height == 0)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Empty rectangle");
                return StatusCode(HttpStatusCode.NoContent);
            }

            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Filter begin");
            img = img.Clone(rect, img.PixelFormat);
            rect.X = 0;
            rect.Y = 0;
            var bytes = rect.Width * rect.Height;
            var argbValues = img.ToArray(rect);
            var filter = ImageFilters.FromString(flt, out var byteLevel);


            for (var i = 0; i < bytes; i++)
            {
                argbValues[i] = filter((uint) argbValues[i], byteLevel);
            }

            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Filter end");

            return await Task.FromResult(new OkResult(argbValues.ToBitmap(rect.Width, rect.Height),
                Request.Content.Headers.ContentType.MediaType));
        }
    }
}