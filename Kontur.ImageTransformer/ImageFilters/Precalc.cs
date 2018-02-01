using System;
using static System.Linq.Enumerable;

namespace Kontur.ImageTransformer.ImageFilters
{
    public class Precalc
    {
        //argb
        public static readonly int[] GrayInt = (from r in Range(0x00, 0x100)
            from g in Range(0x00, 0x100)
            from b in Range(0x00, 0x100)
            select (((GrayPix(r, g, b) << 8) + GrayPix(r, g, b)) << 8) + GrayPix(r, g, b)
        ).ToArray();

        public static readonly int[] SepiaInt = (from r in Range(0x00, 0x100)
            from g in Range(0x00, 0x100)
            from b in Range(0x00, 0x100)
            select (((SepiaR(r, g, b) << 8) + SepiaG(r, g, b)) << 8) + SepiaB(r, g, b)
        ).ToArray();


        private static int GrayPix(int r, int g, int b) => (r + g + b) / 3;

        private static int SepiaR(int r, int g, int b) => Math.Min((int) (r * .393 + g * .769 + b * .189), 255);

        private static int SepiaG(int r, int g, int b) => Math.Min((int) (r * .349 + g * .686 + b * .168), 255);

        private static int SepiaB(int r, int g, int b) => Math.Min((int) (r * .272 + g * .534 + b * .131), 255);
    }
}