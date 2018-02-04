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
    public class MainCheckHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method != HttpMethod.Post ||
                request.Content == null ||
                request.Content.Headers.ContentLength <= 0 ||
                request.Content.Headers.ContentLength > 102400)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var contentType = request.Content.Headers.ContentType;
            if (!string.Equals("image/png", contentType.MediaType, StringComparison.OrdinalIgnoreCase) &&
                !string.Equals("application/octet-stream", contentType.MediaType, StringComparison.OrdinalIgnoreCase))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}