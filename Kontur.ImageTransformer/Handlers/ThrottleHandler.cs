using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.ImageTransformer.Handlers
{
    public class ThrottleHandler : DelegatingHandler
    {
        private readonly HandleRequest _handleRequest;

        public ThrottleHandler()
        {
            _handleRequest = new HandleRequest();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var allowRequest = await _handleRequest.IsValidRequest(request);
            if (!allowRequest)
            {
                return request.CreateResponse((HttpStatusCode) 429);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}