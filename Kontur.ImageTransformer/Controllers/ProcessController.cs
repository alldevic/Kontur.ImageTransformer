using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using Kontur.ImageTransformer.Results;

namespace Kontur.ImageTransformer.Controllers
{
    [OverrideAuthentication, OverrideAuthorization, AllowAnonymous]
    [RoutePrefix("process")]
    public class ProcessController : ApiController
    {
        [HttpPost, Route("rotate-cw/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<IHttpActionResult> Grayscale(int x, int y, int w, int h)
        {
            var tracer = Request.GetConfiguration().Services.GetTraceWriter();

            if (!Request.TryToBitmap(out var img) || img.PixelFormat != PixelFormat.Format32bppArgb ||
                img.Width > 1000 || img.Height > 1000)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Incorrect PNG");
                return BadRequest();
            }

            #region Not Working

            var plot = Rectangle.Intersect(new Rectangle(x, y, w, h), new Rectangle(0, 0, img.Width, img.Height));
            if (plot.IsEmpty || plot.Width == 0 || plot.Height == 0)
            {
                tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Empty rectangle");
                return StatusCode(HttpStatusCode.NoContent);
            }

            img.Crop(plot);

            #endregion

            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            tracer.Info(Request, ControllerContext.ControllerDescriptor.ControllerType.FullName, "Filter end");
            return await Task.FromResult(new OkResult(img));
        }

        //rotate-ccw: Rotate270FlipNone
        //flip-h: RotateNoneFlipX
        //flip-v: RotateNoneFlipY
    }
}