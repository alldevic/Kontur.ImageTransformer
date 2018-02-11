using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Kontur.ImageTransformer;
using Kontur.ImageTransformer.Handlers;
using MyTested.WebApi;
using NUnit.Framework;

namespace Kontur.ImageTransformerTests.HandlersTests
{
    [TestFixture]
    public class MainCheckHandlerTests
    {
        private static IEnumerable HttpMethodCases()
        {
            var mimeTypes = new List<(string method, HttpStatusCode code)>()
            {
                ("GET", HttpStatusCode.BadRequest),
                ("HEAD", HttpStatusCode.BadRequest),
                ("POST", HttpStatusCode.OK),
                ("PUT", HttpStatusCode.BadRequest),
                ("DELETE", HttpStatusCode.BadRequest),
                ("CONNECT", HttpStatusCode.BadRequest),
                ("OPTIONS", HttpStatusCode.BadRequest),
                ("TRACE", HttpStatusCode.BadRequest),
                ("PATCH", HttpStatusCode.BadRequest)
            };


            foreach (var mime in mimeTypes)
            {
                yield return new TestCaseData(mime.method, mime.code);
            }
        }

        [TestCaseSource(nameof(HttpMethodCases))]
        public void IncorrectHttpMethods(string method, HttpStatusCode code)
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(method)
                        .WithRequestUri("/process/grayscale/0,0,200,200")
                        .WithByteArrayContent(Utils.OnePixPng())
                        .WithContentHeader(HttpContentHeader.ContentType, "image/png")
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(code);
        }

        [Test]
        public void NullableContent()
        {
            MyWebApi.Handler<MainCheckHandler>()
                .WithHttpRequestMessage(request => request.WithContent(null))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        private static IEnumerable LengthCases()
        {
            var mimeTypes = new List<(string length, HttpStatusCode code)>()
            {
                //("-1", HttpStatusCode.BadRequest), //Incorrect format 
                ("0", HttpStatusCode.BadRequest),
                ("1", HttpStatusCode.OK),
                ("51200", HttpStatusCode.OK),
                ("102400", HttpStatusCode.OK),
                ("102401", HttpStatusCode.BadRequest)
            };


            foreach (var mime in mimeTypes)
            {
                yield return new TestCaseData(mime.length, mime.code);
            }
        }

        [TestCaseSource(nameof(LengthCases))]
        public void ContentLengthTests(string length, HttpStatusCode code)
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(HttpMethod.Post)
                        .WithRequestUri("/process/grayscale/0,0,200,200")
                        .WithByteArrayContent(Utils.OnePixPng())
                        .WithContentHeader(HttpContentHeader.ContentType, "image/png")
                        .WithContentHeader(HttpContentHeader.ContentLength, length)
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(code);
        }


        private static IEnumerable MimeCases()
        {
            var mimeTypes = new List<(string mime, HttpStatusCode code)>()
            {
                ("image/png", HttpStatusCode.OK),
                ("application/octet-stream", HttpStatusCode.OK),
                ("image/jpeg", HttpStatusCode.BadRequest),
                ("application/json", HttpStatusCode.BadRequest),
                ("text/html", HttpStatusCode.BadRequest),
                ("text/plain", HttpStatusCode.BadRequest),
                ("application/xml", HttpStatusCode.BadRequest)
            };


            foreach (var mime in mimeTypes)
            {
                yield return new TestCaseData(mime.mime, mime.code);
            }
        }

        [TestCaseSource(nameof(MimeCases))]
        public void MediaTypesTests(string mime, HttpStatusCode code)
        {
            MyWebApi.Server().Working(Configuration.SetConfiguration())
                .WithHttpRequestMessage(
                    request => request.WithMethod(HttpMethod.Post)
                        .WithRequestUri("/process/grayscale/0,0,200,200")
                        .WithByteArrayContent(Utils.OnePixPng())
                        .WithContentHeader(HttpContentHeader.ContentType, mime)
                )
                .ShouldReturnHttpResponseMessage().WithStatusCode(code);
        }
    }
}