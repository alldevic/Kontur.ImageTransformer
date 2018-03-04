// Based on https://github.com/aspnet/AspNetWebStack/blob/master/test/Common/Routing/RouteConstraintsTests.cs

using System;
using System.Net.Http;
using System.Web.Http.Routing;
using Kontur.ImageTransformer.Constraints;
using NUnit.Framework;

namespace Kontur.ImageTransformer.Tests
{
    [TestFixture]
    public class ConstraintsTests
    {
        [TestCase("0,0,0,0", true)]
        [TestCase("0.0.0.0", false)]
        [TestCase("1.0,0,0,0", false)]
        [TestCase("00,0,0,0", false)]
        [TestCase("000,00,0,0", false)]
        [TestCase("-0,0,0,0", false)]
        [TestCase("+0,0,0,0", false)]
        [TestCase("0a,0,0,0", false)]
        [TestCase("", false)]
        [TestCase("0", false)]
        [TestCase("abc", false)]
        [TestCase("//?//", false)]
        [TestCase("/n/t/?", false)]
        [TestCase("0,0,0", false)]
        [TestCase("0,0", false)]
        [TestCase("2147483648,0,0,0", true)]
        [TestCase("-2147483648,0,0,0", true)]
        [TestCase("2147483649,0,0,0", false)]
        [TestCase("-2147483649,0,0,0", false)]
        public void CoordsConstraintTests(string parameterValue, bool expected)
        {
            var constraint = new CoordsConstraint();
            var actual = TestValue(constraint, parameterValue);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("flip-h", true)]
        [TestCase("flip-v", true)]
        [TestCase("rotate-cw", true)]
        [TestCase("rotate-ccw", true)]
        [TestCase("", false)]
        [TestCase("/", false)]
        [TestCase("//", false)]
        [TestCase("fliph", false)]
        [TestCase("flipv", false)]
        [TestCase("FLIP-H", false)]
        [TestCase("FlIP=v", false)]
        public void TransformConstraintTests(string parameterValue, bool expected)
        {
            var constraint = new TransformConstraint();
            var actual = TestValue(constraint, parameterValue);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("grayscale", true)]
        [TestCase("sepia", true)]
        [TestCase("threshold(0)", true)]
        [TestCase("threshold(100)", true)]
        [TestCase("threshold(56)", true)]
        [TestCase("thresehold(-1)", false)]
        [TestCase("thresehold(101)", false)]
        [TestCase("qweqwe", false)]
        [TestCase("SePia", false)]
        [TestCase("SEPIA", false)]
        [TestCase("", false)]
        [TestCase("/t/n/", false)]
        [TestCase("/", false)]
        [TestCase("//", false)]
        public void FilterConstraintTests(string parameterValue, bool expected)
        {
            var constraint = new FilterConstraint();
            var actual = TestValue(constraint, parameterValue);
            Assert.AreEqual(expected, actual);
        }


        private static bool TestValue(IHttpRouteConstraint constraint, object value,
            Action<IHttpRoute> routeConfig = null)
        {
            var httpRequestMessage = new HttpRequestMessage();
            var httpRoute = new HttpRoute();
            routeConfig?.Invoke(httpRoute);
            const string parameterName = "fake";
            var values = new HttpRouteValueDictionary {{parameterName, value}};
            const HttpRouteDirection httpRouteDirection = HttpRouteDirection.UriResolution;

            return constraint.Match(httpRequestMessage, httpRoute, parameterName, values, httpRouteDirection);
        }
    }
}