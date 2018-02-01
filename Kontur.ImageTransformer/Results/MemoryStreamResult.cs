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
    /// Write MemoryStream to body of response
    /// </summary>
    public class MemoryStreamResult : IHttpActionResult
    {
        private readonly MemoryStream _value;
        private readonly string _contetntType;

        /// <summary>
        /// Send a content of MemoryStream in body of response with specific Content-Type HTTP header
        /// </summary>
        /// <param name="value">MemoryStream for pushing in reponse</param>
        /// <param name="contetntType">Specific Content-Type HTTP header</param>
        public MemoryStreamResult(MemoryStream value, string contetntType = "application/octet-stream")
        {
            _value = value;
            _contetntType = contetntType;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ByteArrayContent(_value.ToArray()),
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue(_contetntType);

            return await Task.FromResult(response);
        }
    }
}