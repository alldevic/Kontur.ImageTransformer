using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Kontur.ImageTransformerTests
{
    public static class Utils
    {
        public static byte[] OnePixPng()
        {
            var k = new Bitmap(1, 1);
            k.SetPixel(0, 0, Color.Chartreuse);
            var ms = new MemoryStream();
            k.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}