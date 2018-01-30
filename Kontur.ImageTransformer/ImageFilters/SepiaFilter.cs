namespace Kontur.ImageTransformer.ImageFilters
{
    public class SepiaFilter : IPixelFilter
    {
        public int Set(int pixel) =>
            (int) (Precalc.SepiaInt[pixel & 0x00FFFFFF] | pixel & 0xFF000000);
    }
}