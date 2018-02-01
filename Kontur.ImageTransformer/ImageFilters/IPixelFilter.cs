namespace Kontur.ImageTransformer.ImageFilters
{
    /// <summary>
    /// Base functional for every filter
    /// </summary>
    public interface IPixelFilter
    {
        /// <summary>
        /// Filtering current pixel
        /// </summary>
        /// <param name="pixel">Pixel for filtering</param>
        /// <returns>Filtered pixel</returns>
        int Set(int pixel);
    }
}