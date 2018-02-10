// TODO: Add content check (now only status code and length)
// TODO: max content length
// TODO: add rectangle tests

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Kontur.ImageTransformer;
using MyTested.WebApi;
using NUnit.Framework;

namespace Kontur.ImageTransformerTests
{
    /// <summary>
    /// Test assembly as black box: request - response.
    /// Every request on new server 
    /// </summary>
    [TestFixture]
    public class PipelineTests
    {
        private static IEnumerable FilterCases()
        {
            var filters = new List<string> {"grayscale", "sepia"};
            filters.AddRange(Enumerable.Range(0, 101).Select(thr => $"threshold({thr})"));
            var types = new List<string> {"image/png", "application/octet-stream"};

            foreach (var flt in filters)
            foreach (var type in types)
            {
                yield return new TestCaseData(flt, type);
            }
        }

        [TestCaseSource(nameof(FilterCases))]
        public void CorrectFilterRequest(string filter, string contentType)
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(HttpMethod.Post)
                        .WithRequestUri($"/process/{filter}/0,0,200,200")
                        .WithByteArrayContent(Utils.OnePixPng())
                        .WithContentHeader(HttpContentHeader.ContentType, contentType)
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(HttpStatusCode.OK)
                .ContainingContentHeader(HttpContentHeader.ContentLength, Utils.OnePixPng().Length.ToString());
        }


        [Test]
        public void NullContent()
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(HttpMethod.Post)
                        .WithRequestUri($"/process/grayscale/0,0,200,200")
                        .WithByteArrayContent(new byte[] { })
                        .WithContentHeader(HttpContentHeader.ContentType, "image/png")
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(HttpStatusCode.BadRequest);
        }

        [TestCase("/process/g/0,0")]
        [TestCase("/")]
        [TestCase("/p/grayscale/0,0,0,0")]
        [TestCase("/process/filter/0,0,0,0")]
        [TestCase("/process/grayscale/,0,0,0")]
        [TestCase("/process/grayscale/0,,0,0")]
        [TestCase("/process/grayscale/0,0,,0")]
        [TestCase("/process/grayscale/0,0,0,")]
        [TestCase("/process/grayscale/0.0.0.0")]
        [TestCase("/process/grayscale/x,y,w,h")]
        [TestCase("/process/grayscale/")]
        [TestCase("/process/")]
        [TestCase("/process/grayscale/coords")]
        [TestCase("/process/grayscale/2147483649,2147483649,2147483649,2147483649")]
        [TestCase("/process/grayscale/-2147483649,-2147483649,-2147483649,-2147483649")]
        public void IncorrectRoute(string route)
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(HttpMethod.Post)
                        .WithRequestUri(route)
                        .WithByteArrayContent(Utils.OnePixPng())
                        .WithContentHeader(HttpContentHeader.ContentType, "image/png")
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(HttpStatusCode.BadRequest);
        }
    }
}