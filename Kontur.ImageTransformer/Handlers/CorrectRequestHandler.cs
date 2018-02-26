using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.ImageTransformer.Handlers
{
    /// <inheritdoc />
    /// <summary>
    /// Check for method, correct Content-Length, header and nullable content
    /// Accept POST, image/png, application/octet-stream, content-length in (0; 102400)
    /// </summary>
    public class CorrectRequestHandler : DelegatingHandler
    {
        private readonly TokenBucket _bucket;

        public CorrectRequestHandler()
        {
            _bucket = new TokenBucket(102400 * Environment.ProcessorCount * 25, 900);
        }

        public CorrectRequestHandler(TokenBucket bucket)
        {
            _bucket = bucket;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!(MainCheck(request) && ContentTypeCheck(request) && ThrottleCheck(request)))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private static bool MainCheck(HttpRequestMessage request)
        {
            return request.Method == HttpMethod.Post && request.Content?.Headers.ContentLength != null &&
                   !(request.Content.Headers.ContentLength <= 0) && !(request.Content.Headers.ContentLength > 102400);
        }

        private static bool ContentTypeCheck(HttpRequestMessage request)
        {
            try
            {
                var contentType = request.Content.Headers.ContentType;
                if (!string.Equals("image/png", contentType.MediaType, StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals("application/octet-stream", contentType.MediaType,
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool ThrottleCheck(HttpRequestMessage request)
        {
            if (request.Content.Headers.ContentLength != null)
            {
                return !_bucket.ShouldThrottle(request.Content.Headers.ContentLength.Value);
            }

            return false;
        }
    }
}