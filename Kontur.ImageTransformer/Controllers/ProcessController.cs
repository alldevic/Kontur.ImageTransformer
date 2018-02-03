using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Kontur.ImageTransformer.Formatters;

namespace Kontur.ImageTransformer.Controllers
{
    [OverrideAuthentication, OverrideAuthorization]
    public class ProcessController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Grayscale(int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.GrayscaleFilter);

        [HttpPost, Route("process/threshold({level:int:range(0,100)})/{x},{y},{w},{h}")]
        public async Task<IHttpActionResult> Threshold(byte level, int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.ThresholdFilter, level);

        [HttpPost]
        public async Task<IHttpActionResult> Sepia(int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.SepiaFilter);

        private async Task<IHttpActionResult> Do(int x, int y, int w, int h, ImageFilters.Filter filter, int level = 0)
        {
            if (!Request.TryToBitmap(out var img))
            {
                return await Task.FromResult(BadRequest());
            }

            var plot = Rectangle.Intersect(new Rectangle(x, y, w, h), new Rectangle(0, 0, img.Width, img.Height));
            if (plot.IsEmpty || plot.Width == 0 || plot.Height == 0)
            {
                return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
            }

            var bytes = plot.Width * plot.Height;
            var argbValues = img.ToArray(plot);

            var byteLevel = (byte) (255 * level / 100);
            int i;
            for (i = 0; i < bytes; i++)
            {
                argbValues[i] = filter(argbValues[i], byteLevel);
            }

            img = argbValues.ToBitmap(plot.Width, plot.Height);

#if filesave
            img.Save("file.png", ImageFormat.Png);
#endif

            return await Task.FromResult(Content(HttpStatusCode.OK, img, new BitmapWriteFormatter()));
        }
    }
}