using System.Net;
using Kontur.ImageTransformer.Handlers;
using MyTested.WebApi;
using NUnit.Framework;

namespace Kontur.ImageTransformerTests.HandlersTests
{
    [TestFixture]
    public class MainCheckHandlerTests
    {
        [TestCase("GET")]
        [TestCase("HEAD")]
        [TestCase("PUT")]
        [TestCase("DELETE")]
        [TestCase("CONNECT")]
        [TestCase("OPTIONS")]
        [TestCase("TRACE")]
        [TestCase("PATCH")]
        public void IncorrectHttpMethods(string method)
        {
            MyWebApi.Handler<MainCheckHandler>()
                .WithHttpRequestMessage(request => request.WithMethod(method))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Ignore("Not implemented")]
        [TestCase("POST")]
        public void CorrectHttpMethods(string method)
        {
        }

        [Test]
        public void NullableContent()
        {
            MyWebApi.Handler<MainCheckHandler>()
                .WithHttpRequestMessage(request => request.WithContent(null))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Ignore("Not implemented")]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(102401)]
        public void IncorrectContentLength(int length)
        {
        }

        [Ignore("Not implemented")]
        [TestCase(1)]
        [TestCase(51200)]
        [TestCase(102400)]
        public void CorrectContentLength(int length)
        {
        }

        [Ignore("Not implemented")]
        [Test]
        public void CorrectMediaTypes()
        {
        }


        [Ignore("Not implemented")]
        [TestCase("image/jpeg")]
        [TestCase("text/html")]
        [TestCase("text/plain")]
        [TestCase("video/mpeg")]
        [TestCase("application/pdf")]
        public void SomeIncorrectMediaTypes(string mime)
        {
        }
    }
}