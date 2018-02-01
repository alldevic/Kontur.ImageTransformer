//#define debug

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using Kontur.ImageTransformer.ImageFilters;
using Kontur.ImageTransformer.Results;

namespace Kontur.ImageTransformer.Controllers
{
    [OverrideAuthentication, OverrideAuthorization]
    public class ProcessController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Grayscale(int x, int y, int w, int h) =>
            await Do(x, y, w, h, new GrayscaleFilter());

        [HttpPost, Route("process/threshold({level:int:range(0,100)})/{x},{y},{w},{h}")]
        public async Task<IHttpActionResult> Threshold(byte level, int x, int y, int w, int h) =>
            await Do(x, y, w, h, new ThresholdFilter(level));

        [HttpPost]
        public async Task<IHttpActionResult> Sepia(int x, int y, int w, int h) =>
            await Do(x, y, w, h, new SepiaFilter());

        /*[HttpPost]
        public async Task<IHttpActionResult> Crop(int x, int y, int w, int h) =>
            await Do(x, y, w, h, new EqualFilter());*/

        private async Task<IHttpActionResult> Do(int x, int y, int w, int h, IPixelFilter filter)
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

            for (var i = 0; i < bytes; i++)
            {
                argbValues[i] = filter.Set(argbValues[i]);
            }

            img = new Bitmap(plot.Width, plot.Height, PixelFormat.Format32bppArgb);
            bmpData = img.LockBits(new Rectangle(0, 0, plot.Width, plot.Height),
                ImageLockMode.WriteOnly, img.PixelFormat);
            Marshal.Copy(argbValues, 0, bmpData.Scan0, bytes);
            img.UnlockBits(bmpData);

            using (var res = new MemoryStream())
            {
                img.Save(res, ImageFormat.Png);
                return await Task.FromResult(new MemoryStreamResult(res));
            }
        }
    }
}