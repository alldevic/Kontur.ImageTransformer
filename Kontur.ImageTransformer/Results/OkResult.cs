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
        private readonly string _contentType;

        /// <summary> 
        /// Send a content of Bitmap in body of response with specific Content-Type HTTP header 
        /// </summary> 
        /// <param name="value">Bitmap for pushing in reponse</param> 
        /// <param name="contentType">Specific Content-Type HTTP header</param> 
        public OkResult(Bitmap value, string contentType = "application/octet-stream")
        {
            _value = value;
            _contentType = contentType;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            var ms = new MemoryStream();
            _value.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
            return await Task.FromResult(response);
        }
    }
}