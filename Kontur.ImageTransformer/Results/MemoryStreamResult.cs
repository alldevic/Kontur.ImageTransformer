using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kontur.ImageTransformer.Results
{
    /// <inheritdoc />
    /// <summary>
    /// Write MemoryStram to body of response
    /// </summary>
    public class MemoryStreamResult : IHttpActionResult
    {
        private readonly MemoryStream _value;

        public MemoryStreamResult(MemoryStream value)
        {
            _value = value;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ByteArrayContent(_value.ToArray()),
            };
            
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return Task.FromResult(response);
        }
    }
}