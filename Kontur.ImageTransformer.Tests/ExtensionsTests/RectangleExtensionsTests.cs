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
        
        
    }
}