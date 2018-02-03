#define filesave

using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
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

#if DEBUG
        [HttpPost]
        public async Task<IHttpActionResult> Crop(int x, int y, int w, int h) =>
            await Do(x, y, w, h, ImageFilters.EqualFilter);
#endif

        private async Task<IHttpActionResult> Do(int x, int y, int w, int h, ImageFilters.Filter filter, int level = 0)
        {
            Bitmap img;

            try
            {
                img = new Bitmap(Request.Content.ReadAsStreamAsync().Result);
            }
            catch
            {
                return await Task.FromResult(BadRequest());
            }

            var plot = Rectangle.Intersect(new Rectangle(x, y, w, h), new Rectangle(0, 0, img.Width, img.Height));
            if (plot.IsEmpty || plot.Width == 0 || plot.Height == 0)
            {
                return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
            }

            var bytes = plot.Width * plot.Height;
            var argbValues = new int[bytes];

            var bmpData = img.LockBits(plot, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(bmpData.Scan0, argbValues, 0, bytes);
            img.UnlockBits(bmpData);
            var byteLevel = (byte) (255 * level / 100);
            int i;
            for (i = 0; i < bytes; i++)
            {
                argbValues[i] = filter(argbValues[i], byteLevel);
            }


            img = new Bitmap(plot.Width, plot.Height, PixelFormat.Format32bppArgb);
            bmpData = img.LockBits(new Rectangle(0, 0, plot.Width, plot.Height),
                ImageLockMode.WriteOnly, img.PixelFormat);
            Marshal.Copy(argbValues, 0, bmpData.Scan0, bytes);
            img.UnlockBits(bmpData);

#if filesave
            img.Save("file.png", ImageFormat.Png);
#endif

            return await Task.FromResult(Content(HttpStatusCode.OK, img, new BitmapWriteFormatter()));
        }
    }
}