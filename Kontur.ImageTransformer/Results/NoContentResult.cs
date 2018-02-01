using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kontur.ImageTransformer.Results
{
    /// <summary>
    /// Return correct 204 NoContent
    /// </summary>
    public class NoContentResult : IHttpActionResult
    {
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) =>
            await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
    }
}