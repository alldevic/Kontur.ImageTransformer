namespace Kontur.ImageTransformer.ImageFilters
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of grayscale filter. It's not grayscale, it's AVERAGE filter:
    /// R = G = B = (oldR + oldG + oldB) / 3 
    /// </summary>
    public class GrayscaleFilter : IPixelFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// Filtering pixel with save alpha channel. 
        /// </summary>
        /// <param name="pixel">Pixel in ARGB format</param>
        /// <returns>Filtered pixel with save alpha channel from original</returns>
        public int Set(int pixel) =>
            (int) (Precalc.GrayInt[pixel & 0x00FFFFFF] | pixel & 0xFF000000);
    }
}