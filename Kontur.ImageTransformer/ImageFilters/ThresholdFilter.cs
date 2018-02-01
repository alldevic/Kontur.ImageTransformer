using System;

namespace Kontur.ImageTransformer.ImageFilters
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of threshold filter. Get a last byte from precalculated average filter values.
    /// If (AVG[pix] >= level)
    ///     R = G = B = 0xFF
    /// else
    ///     R = G = B = 0x0
    /// </summary>
    public class ThresholdFilter : IPixelFilter
    {
        /// <summary>
        /// Intensity of filter
        /// </summary>
        private readonly int _level;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="level">Intensity of threshold filter in procentage</param>
        public ThresholdFilter(int level = 50)
        {
            if (level < 0 || level > 100)
            {
                throw new ArgumentOutOfRangeException($"Invalid threshold level: {nameof(level)}");
            }

            _level = 255 * level / 100;
        }

        /// <inheritdoc />
        /// <summary>
        /// Filtering pixel with save alpha channel
        /// </summary>
        /// <param name="pixel">Pixel in ARGB format</param>
        /// <returns>Filtered pixel</returns>
        public int Set(int pixel)
        {
            var k = Precalc.GrayInt[pixel & 0x00FFFFFF] & 0x000000FF;

            return (int) ((k < _level ? 0x0 : 0xFFFFFF) | pixel & 0xFF000000);
        }
    }
}