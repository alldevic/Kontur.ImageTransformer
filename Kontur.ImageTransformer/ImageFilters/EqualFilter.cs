#if DEBUG

namespace Kontur.ImageTransformer.ImageFilters
{
    /// <inheritdoc />
    /// <summary>
    /// Implement a equal filter. For testing crop and perfomance without math operations 
    /// </summary>
    public class EqualFilter : IPixelFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// Simply return argument 
        /// </summary>
        /// <param name="pixel">Pixel in any format</param>
        /// <returns>Return argument without changes</returns>
        public int Set(int pixel) => pixel;
    }
}

#endif