using System.Net.Http;
using Kontur.ImageTransformer.Controllers;
using MyTested.WebApi;
using NUnit.Framework;

namespace Kontur.ImageTransformerTests
{
    [TestFixture]
    public class RouteTests
    {
        /*private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new HttpClient {BaseAddress = new Uri("http://localhost:8080/")};
        }

        #region BadRequestTests

        [Test]
        public void GetCorrectRouteTest()
        {
            var response = _client.GetAsync("/process/grayscale/0,0,0,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void GetIncorrectRouteTest()
        {
            var response = _client.GetAsync("/process/g/0,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostIncorrectRouteTest()
        {
            var response = _client.GetAsync("/").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostIncorrectControllerTest()
        {
            var response = _client.GetAsync("/p/grayscale/0,0,0,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostIncorrectActionTest()
        {
            var response = _client.GetAsync("/process/blacked/0,0,0,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostWithoutXTest()
        {
            var response = _client.GetAsync("/process/grayscale/,0,0,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostWithoutYTest()
        {
            var response = _client.GetAsync("/process/grayscale/0,,0,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostWithoutWidthTest()
        {
            var response = _client.GetAsync("/process/grayscale/0,0,,0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostWithoutHeightTest()
        {
            var response = _client.GetAsync("/process/grayscale/0,0,0,").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostIncorrectCoordsSeparatorTest()
        {
            var response = _client.GetAsync("/process/grayscale/0.0.0.0").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostCoordsNotNumberTest()
        {
            var response = _client.GetAsync("/process/grayscale/x,y,w,h").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostWithoutCoordsTest()
        {
            var response = _client.GetAsync("/process/grayscale/").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostMaxCoordsTest()
        {
            var response = _client.GetAsync("/process/grayscale/2147483649,2147483649,2147483649,2147483649").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        [Test]
        public void PostMinCoordsTest()
        {
            var response = _client.GetAsync("/process/grayscale/-2147483649,-2147483649,-2147483649,-2147483649")
                .Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(0, response.Content.Headers.ContentLength);
        }

        #endregion*/
        [Test]
        public void SepiaMap()
        {
            MyWebApi.Routes().ShouldMap("process/sepia/1,2,3,4").WithHttpMethod(HttpMethod.Post)
                .To<ProcessController>(c => c.Sepia(1, 2, 3, 4));
        }

        [Test]
        public void GrayscaleMap()
        {
            MyWebApi.Routes().ShouldMap("process/grayscale/1,2,3,4").WithHttpMethod(HttpMethod.Post)
                .To<ProcessController>(c => c.Grayscale(1, 2, 3, 4));
        }
        
        [Test]
        public void ThresholdMap()
        {
            MyWebApi.Routes().ShouldMap("process/threshold(5)/1,2,3,4").WithHttpMethod(HttpMethod.Post)
                .To<ProcessController>(c => c.Threshold(5, 1, 2, 3, 4));
        }
    }
}