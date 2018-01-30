using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Kontur.ImageTransformer.ImageFilters;
using Kontur.ImageTransformer.Results;

namespace Kontur.ImageTransformer
{
    public static class Filtering
    {
        public static async Task<IHttpActionResult> Do(Stream stream, int x, int y, int w, int h, IPixelFilter filter)
        {
            var img = new Bitmap(stream);

            var ir = Rectangle.Intersect(new Rectangle(x, y, w, h), new Rectangle(0, 0, img.Width, img.Height));
            if (ir.IsEmpty || ir.Width == 0 || ir.Height == 0)
            {
                return await Task.FromResult(new NoContentResult());
            }

            SetFilter(img, ir, filter);

            var res = new MemoryStream();
            img.Clone(ir, img.PixelFormat).Save(res, ImageFormat.Png);

            return await Task.FromResult(new MemoryStreamResult(res));
        }

        private static void SetFilter(Bitmap img, Rectangle area, IPixelFilter filter)
        {
            var bmpData = img.LockBits(area, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            var ptr = bmpData.Scan0;
            var bytes = img.Width * img.Height;
            var argbValues = new int[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, bytes);

            int i;
            for (i = 0; i < bytes - 4; i += 5)
            {
                argbValues[i] = filter.Set(argbValues[i]);
                argbValues[i + 1] = filter.Set(argbValues[i + 1]);
                argbValues[i + 2] = filter.Set(argbValues[i + 2]);
                argbValues[i + 3] = filter.Set(argbValues[i + 3]);
                argbValues[i + 4] = filter.Set(argbValues[i + 4]);
            }

            for (; i < bytes; i++)
            {
                argbValues[i] = filter.Set(argbValues[i]);
            }

            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, bytes);
            img.UnlockBits(bmpData);
        }
    }
}