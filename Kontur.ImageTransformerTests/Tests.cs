using NUnit.Framework;
using Kontur.ImageTransformer;

namespace Kontur.ImageTransformerTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GcServerTest()
        {
            Assert.IsTrue(Program.Testcase);
        }
    }
}
