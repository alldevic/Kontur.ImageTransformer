using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kontur.ImageTransformer.Results
{
    /// <inheritdoc />
    /// <summary>
    /// Write byte[] to body of response
    /// </summary>
    public class ByteArrayResult : IHttpActionResult
    {
        private readonly byte[] _value;
        private readonly string _contetntType;

        /// <summary>
        /// Send byte[] in body of response with specific Content-Type HTTP header
        /// </summary>
        /// <param name="value">Byte[] for pushing in reponse</param>
        /// <param name="contetntType">Specific Content-Type HTTP header</param>
        public ByteArrayResult(byte[] value, string contetntType = "application/octet-stream")
        {
            _value = value;
            _contetntType = contetntType;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ByteArrayContent(_value),
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue(_contetntType);

            return await Task.FromResult(response);
        }
    }
}