using System;
using System.Drawing;
using System.Net.Http;

namespace Kontur.ImageTransformer
{
    public static class Extensions
    {
        /// <summary>
        ///  Convert Request content to Bitmap
        /// </summary>
        /// <param name="request"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static bool TryToBitmap(this HttpRequestMessage request, out Bitmap img)
        {
            try
            {
                img = new Bitmap(request.Content.ReadAsStreamAsync().Result);
            }
            catch
            {
                img = null;
                return false;
            }

            return true;
        }

        public static void Crop(this Bitmap img, Rectangle area)
        {
            throw new NotImplementedException();
        }
    }
}