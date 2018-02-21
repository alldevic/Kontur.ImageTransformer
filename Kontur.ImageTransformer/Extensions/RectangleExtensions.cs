using System;
using System.Drawing;

namespace Kontur.ImageTransformer.Extensions
{
    public static class RectangleExtensions
    {
        public static Rectangle Normalise(this Rectangle rect)
        {
            if (rect.Width < 0)
            {
                rect.X += rect.Width;
                rect.Width = -rect.Width;
            }

            if (rect.Height < 0)
            {
                rect.Y += rect.Height;
                rect.Height = -rect.Height;
            }

            return rect;
        }


        /// <summary>
        /// Partially implementing Image.RotateFlip (RotateFlipType) from System.Drawing for Rectangle.
        /// Incorrect for default usage, only Kontur.ImageTransformer
        /// </summary>
        /// <param name="rect">Rectangle for transformation</param>
        /// <param name="rotateFlipType">COmbination of rotate and flip</param>
        /// <exception cref="NotFiniteNumberException">I implemented only needed consts</exception>
        public static Rectangle RotateFlip(this Rectangle rect, RotateFlipType rotateFlipType, int srcW, int srcH)
        {
            int t;
            Console.WriteLine(rotateFlipType);
            switch (rotateFlipType)
            {
                case RotateFlipType.RotateNoneFlipNone: // == Rotate180FlipXY
                    break;
                case RotateFlipType.Rotate90FlipNone: // == Rotate270FlipXY cw
                    rect.X = rect.Y;
                    rect.Y = rect.Y;
                    rect.Width = -rect.Width;
                    rect.Height = rect.Width;
                    break;
                case RotateFlipType.Rotate270FlipNone: // == Rotate90FlipXY ccw
                    rect.X += rect.Width;
                    rect.Y = rect.Y;
                    t = rect.Width;
                    rect.Width = -rect.Height;
                    rect.Height = t;
                    break;
                case RotateFlipType.RotateNoneFlipX: // == Rotate180FlipY h
                    rect.X = srcW - rect.X;
                    rect.Y = rect.Y;
                    rect.Width = -rect.Width;
                    rect.Height = rect.Height;
                    break;
                case RotateFlipType.RotateNoneFlipY: // == Rotate180FlipX v
                    rect.X = rect.X;
                    rect.Y = srcH - rect.Y;
                    rect.Width = rect.Width;
                    rect.Height = -rect.Height;
                    break;
                default:
                    throw new NotFiniteNumberException();
            }

            return rect;
            
        }
    }
}