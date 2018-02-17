// Content length in handler
// Content and rectangle checks in  filters and process controller

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

            foreach (var flt in filters)
            {
                yield return new TestCaseData(flt);
            }
        }

        [TestCaseSource(nameof(FilterCases))]
        public void CorrectFilterRequest(string filter)
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(HttpMethod.Post)
                        .WithRequestUri($"/process/{filter}/0,0,200,200")
                        .WithByteArrayContent(Utils.OnePixPng())
                        .WithContentHeader(HttpContentHeader.ContentType, "image/png")
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(HttpStatusCode.OK);
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