using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.ImageTransformer.MessageHandlers
{
    /// <inheritdoc />
    /// <summary>
    /// Accept only POST messages
    /// </summary>
    public class PostOnlyHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method != HttpMethod.Post)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}