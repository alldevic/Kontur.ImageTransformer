using System;
using System.Drawing;

namespace Kontur.ImageTransformer.Extensions
{
    public static class RectangleExtensions
    {
        /// <summary>
        /// Correcting a rectangle withe negative width and height
        /// </summary>
        /// <param name="rect">Rectangle for normilising</param>
        /// <returns>Normilised rectangle</returns>
        public static Rectangle Normalise(this Rectangle rect)
        {
            if (rect.Width < 0)
            {
                rect.X += rect.Width;
                rect.Width = -rect.Width;
            }

            if (rect.Height >= 0)
            {
                return rect;
            }

            rect.Y += rect.Height;
            rect.Height = -rect.Height;

            return rect;
        }


        /// <summary>
        /// Partially implementing Image.RotateFlip (RotateFlipType) from System.Drawing for Rectangle.
        /// Incorrect for default usage, only Kontur.ImageTransformer
        /// </summary>
        /// <param name="rect">Rectangle for transformation</param>
        /// <param name="rotateFlipType">Combination of rotate and flip</param>
        /// <param name="srcW">Width of original image</param>
        /// <param name="srcH">Height of original image</param>
        /// <exception cref="NotImplementedException">Implemented only needed consts</exception>
        public static Rectangle RotateFlip(this Rectangle rect, RotateFlipType rotateFlipType, int srcW, int srcH)
        {
            int t;
            switch (rotateFlipType)
            {
                case RotateFlipType.RotateNoneFlipNone: // == Rotate180FlipXY
                    break;
                case RotateFlipType.Rotate90FlipNone: // == Rotate270FlipXY cw
                    t = rect.Y;
                    rect.Y = srcH - rect.Right;
                    rect.X = t;

                    t = rect.Width;
                    rect.Width = rect.Height;
                    rect.Height = t;
                    break;
                case RotateFlipType.Rotate270FlipNone: // == Rotate90FlipXY ccw
                    t = rect.X;
                    rect.X = srcW - rect.Bottom;
                    rect.Y = t;

                    t = rect.Width;
                    rect.Width = rect.Height;
                    rect.Height = t;
                    break;
                case RotateFlipType.RotateNoneFlipX: // == Rotate180FlipY h
                    rect.X = srcW - rect.Right;
                    rect.Y = rect.Y;
                    rect.Width = rect.Width;
                    rect.Height = rect.Height;
                    break;
                case RotateFlipType.RotateNoneFlipY: // == Rotate180FlipX v
                    rect.X = rect.X;
                    rect.Y = srcH - rect.Bottom;
                    rect.Width = rect.Width;
                    rect.Height = rect.Height;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return rect;
        }
    }
}