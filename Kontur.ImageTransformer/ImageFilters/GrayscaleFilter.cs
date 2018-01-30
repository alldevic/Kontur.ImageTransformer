namespace Kontur.ImageTransformer.ImageFilters
{
    public class GrayscaleFilter : IPixelFilter
    {
        //argb
        public int Set(int pixel) =>
            (int) (Precalc.GrayInt[pixel & 0x00FFFFFF] | pixel & 0xFF000000);
    }
}