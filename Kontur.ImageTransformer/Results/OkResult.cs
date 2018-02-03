using System.Drawing;
using System.Drawing.Imaging;
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
    /// Write Bitmap to body of response 
    /// </summary> 
    public class OkResult : IHttpActionResult
    {
        private readonly Bitmap _value;
        private readonly string _contetntType;

        /// <summary> 
        /// Send a content of Bitmap in body of response with specific Content-Type HTTP header 
        /// </summary> 
        /// <param name="value">Bitmap for pushing in reponse</param> 
        /// <param name="contetntType">Specific Content-Type HTTP header</param> 
        public OkResult(Bitmap value, string contetntType = "application/octet-stream")
        {
            _value = value;
            _contetntType = contetntType;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            using (var ms = new MemoryStream())
            {
                _value.Save(ms, ImageFormat.Png);
                response.Content = new ByteArrayContent(ms.ToArray());
            }

            response.Content.Headers.ContentType = new MediaTypeHeaderValue(_contetntType);

            return await Task.FromResult(response);
        }
    }
}