using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace Kontur.ImageTransformer.Tests
{
    [Ignore("Too slow, run if need")]
    [TestFixture]
    public class ImageFiltersTests
    {
        private static Color GrayscaleFilter(Color pixel)
        {
            var intensity = (pixel.R + pixel.G + pixel.B) / 3;
            return Color.FromArgb(pixel.A, intensity, intensity, intensity);
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void GrayscaleTest()
        {
            foreach (var a in Enumerable.Range(0, 256))
            foreach (var r in Enumerable.Range(0, 256))
            foreach (var g in Enumerable.Range(0, 256))
            foreach (var b in Enumerable.Range(0, 256))
            {
                var t = Color.FromArgb(a, r, g, b);
                Assert.AreEqual(ImageFilters.GrayscaleFilter((uint) t.ToArgb()), GrayscaleFilter(t).ToArgb());
            }
        }


        private static Color ThresholdFilter(Color pixel, byte level)
        {
            var intensity = (pixel.R + pixel.G + pixel.B) / 3;
            if (intensity >= 255 * level / 100)
            {
                return Color.FromArgb(pixel.A, 255, 255, 255);
            }

            return Color.FromArgb(pixel.A, 0, 0, 0);
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void ThresholdTest()
        {
            foreach (var lvl in Enumerable.Range(1, 101).Select(i => (byte) i))
            foreach (var a in Enumerable.Range(0, 256))
            foreach (var r in Enumerable.Range(0, 256))
            foreach (var g in Enumerable.Range(0, 256))
            foreach (var b in Enumerable.Range(0, 256))
            {
                var t = Color.FromArgb(a, r, g, b);

                Assert.AreEqual(ImageFilters.ThresholdFilter((uint) t.ToArgb(), (byte) (255 * lvl / 100)),
                    ThresholdFilter(t, lvl).ToArgb());
            }
        }


        private static Color SepiaFilter(Color pixel)
        {
            var r = (int) ((pixel.R * .393) + (pixel.G * .769) + (pixel.B * .189));
            r = r > 255 ? 255 : r;
            var g = (int) ((pixel.R * .349) + (pixel.G * .686) + (pixel.B * .168));
            g = g > 255 ? 255 : g;
            var b = (int) ((pixel.R * .272) + (pixel.G * .534) + (pixel.B * .131));
            b = b > 255 ? 255 : b;

            return Color.FromArgb(pixel.A, r, g, b);
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void SepiaTest()
        {
            foreach (var a in Enumerable.Range(0, 256))
            foreach (var r in Enumerable.Range(0, 256))
            foreach (var g in Enumerable.Range(0, 256))
            foreach (var b in Enumerable.Range(0, 256))
            {
                var t = Color.FromArgb(a, r, g, b);
                Assert.AreEqual(ImageFilters.SepiaFilter((uint) t.ToArgb()), SepiaFilter(t).ToArgb());
            }
        }
    }
}