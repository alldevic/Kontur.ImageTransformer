using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Kontur.ImageTransformer.Formatters
{
    /// <inheritdoc />
    /// <summary>
    /// Write Bitmap to response body with minimum middle data struct
    /// </summary>
    public class BitmapWriteFormatter : MediaTypeFormatter
    {
        /// <inheritdoc />
        /// <summary>
        /// Allow only octet-stream and png
        /// </summary>
        public BitmapWriteFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png"));
        }

        /// <inheritdoc />
        /// <summary>
        /// Disable read to Bitmap
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool CanReadType(Type type) => false;

        /// <inheritdoc />
        /// <summary>
        /// Accept write Bitmap to stream
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool CanWriteType(Type type) => true;

        /// <summary>
        /// Dirty hack, so ugly
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        /// <param name="content"></param>
        /// <param name="transportContext"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content,
            TransportContext transportContext)
        {
            if (!(value is Bitmap tmp))
            {
                throw new NullReferenceException();
            }

            tmp.Save(stream, ImageFormat.Png);
            return Task.Delay(TimeSpan.Zero);
        }
    }
}