namespace Kontur.ImageTransformer.ImageFilters
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of sepia filter. Give precalculated value from static array.
    /// 
    /// outputRed = (inputRed * .393) + (inputGreen *.769) + (inputBlue * .189)
    /// outputGreen = (inputRed * .349) + (inputGreen *.686) + (inputBlue * .168)
    /// outputBlue = (inputRed * .272) + (inputGreen *.534) + (inputBlue * .131)
    /// 
    /// </summary>
    public class SepiaFilter : IPixelFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// Filtering pixel with save alpha channel.
        /// </summary>
        /// <param name="pixel">Pixel in ARGB format</param>
        /// <returns>Filtered pixel with save alpha channel from original</returns>
        public int Set(int pixel) =>
            (int) (Precalc.SepiaInt[pixel & 0x00FFFFFF] | pixel & 0xFF000000);
    }
}