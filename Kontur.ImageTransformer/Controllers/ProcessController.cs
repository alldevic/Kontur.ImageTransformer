using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
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
            rect.RotateFlip(trn);
            var plot = Rectangle.Intersect(rect, new Rectangle(0, 0, img.Width, img.Height));
            if (plot.IsEmpty || plot.Width == 0 || plot.Height == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            img = img.Clone(plot, img.PixelFormat);
            img.RotateFlip(trn);
            return await Task.FromResult(new OkResult(img));
        }
    }
}