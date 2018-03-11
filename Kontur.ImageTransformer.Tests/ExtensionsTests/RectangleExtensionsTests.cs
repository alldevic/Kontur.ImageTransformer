using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using Kontur.ImageTransformer.Extensions;
using NUnit.Framework;

namespace Kontur.ImageTransformer.Tests.ExtensionsTests
{
    [TestFixture]
    public class RectangleExtensionsTests
    {
        private static IEnumerable NormiliseCases()
        {
            foreach (var x in Enumerable.Range(-1, 2))
            foreach (var y in Enumerable.Range(-1, 2))
            foreach (var w in Enumerable.Range(-1, 2))
            foreach (var h in Enumerable.Range(-1, 2))
            {
                var src = new Rectangle(x, y, w, h);
                var res = new Rectangle(
                    w < 0 ? x + w : x,
                    h < 0 ? y + h : y,
                    Math.Abs(w),
                    Math.Abs(h)
                );

                yield return new TestCaseData(src, res);
            }
        }

        [TestCaseSource(nameof(NormiliseCases))]
        public void NormiliseTests(Rectangle src, Rectangle res)
        {
            Assert.That(src.Normalise(), Is.EqualTo(res));
        }

        [Test]
        public void RotateFlip_cw_Correct()
        {
            var src = new Rectangle(55, 75, 70, 70);
            var res = new Rectangle(75, 45, 70, 70);
            Assert.That(src.RotateFlip(RotateFlipType.Rotate90FlipNone, 250, 170), Is.EqualTo(res));
        }

        [Test]
        public void RotateFlip_ccw_Correct()
        {
            var src = new Rectangle(45, 105, 70, 70);
            var res = new Rectangle(105, 55, 70, 70);
            Assert.That(src.RotateFlip(RotateFlipType.Rotate90FlipNone, 250, 170), Is.EqualTo(res));
        }

        [Test]
        public void RotateFlip_flipv_Correct()
        {
            var src = new Rectangle(105, 45, 70, 70);
            var res = new Rectangle(45, -5, 70, 70);
            Assert.That(src.RotateFlip(RotateFlipType.Rotate90FlipNone, 250, 170), Is.EqualTo(res));
        }

        [Test]
        public void RotateFlip_fliph_Correct()
        {
            var src = new Rectangle(75, 55, 70, 70);
            var res = new Rectangle(55, 25, 70, 70);
            Assert.That(src.RotateFlip(RotateFlipType.Rotate90FlipNone, 250, 170), Is.EqualTo(res));
        }
    }
}