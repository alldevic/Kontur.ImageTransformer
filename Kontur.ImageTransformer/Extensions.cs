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


        public static Rectangle RotateFlip(this Rectangle rect, RotateFlipType rotateFlipType)
        {
            int t;
            switch (rotateFlipType)
            {
                case RotateFlipType.RotateNoneFlipNone: // == Rotate180FlipXY
                    return rect;
                case RotateFlipType.Rotate90FlipNone: // == Rotate270FlipXY
                    rect.X += rect.Width;
                    rect.Y = rect.Y;
                    t = rect.Width;
                    rect.Width = rect.Height;
                    rect.Height = -t;
                    return rect;
                case RotateFlipType.Rotate270FlipNone: // == Rotate90FlipXY
                    rect.X = rect.X;
                    rect.Y += rect.Height;
                    t = rect.Width;
                    rect.Width = -rect.Height;
                    rect.Height = t;
                    return rect;
                case RotateFlipType.RotateNoneFlipX: // == Rotate180FlipY
                    rect.X += rect.Width;
                    rect.Y = rect.Y;
                    rect.Width = -rect.Width;
                    rect.Height = rect.Height;
                    return rect;
                case RotateFlipType.RotateNoneFlipY: // == Rotate180FlipX
                    rect.X = rect.X;
                    rect.Y += rect.Height;
                    rect.Width = rect.Width;
                    rect.Height = -rect.Height;
                    return rect;
                default:
                    throw new NotFiniteNumberException();
            }
        }
    }
}