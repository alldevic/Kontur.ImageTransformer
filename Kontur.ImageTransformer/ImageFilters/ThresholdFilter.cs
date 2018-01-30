namespace Kontur.ImageTransformer.ImageFilters
{
    public class ThresholdFilter : IPixelFilter
    {
        private readonly int _level;

        public ThresholdFilter(byte level)
        {
            _level = 255 * level / 100;
        }

        public int Set(int pixel)
        {
            var t = Precalc.GrayInt[pixel & 0x00FFFFFF];
            var k = t & 0x000000FF;

            return (int) ((k < _level ? 0x0 : 0xFFFFFF) | pixel & 0xFF000000);
        }
    }
}