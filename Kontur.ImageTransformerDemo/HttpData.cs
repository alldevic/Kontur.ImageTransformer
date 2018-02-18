using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace Kontur.ImageTransformerDemo
{
    public class HttpData
    {
        #region Request

        [Category("Request")]
        [Description("Content-Type header of request")]
        [DisplayName("Content-Type")]
        public string ContentType { get; set; } = "image/png";

        [Category("Request")]
        [DisplayName("Threshold level")]
        public int ThresholdLevel { get; set; } = 50;

        [Category("Request")]
        [DisplayName("Coordinates")]
        public Rectangle Coords { get; set; } = Rectangle.Empty;

        [Category("Request")]
        [DisplayName("HTTP Method")]
        public string Method { get; set; } = "POST";

        [Category("Request")]
        [DisplayName("Body")]
        public Image RequestImage { get; set; } = new Bitmap(250, 250, PixelFormat.Format32bppArgb);

        [Category("Request")]
        [DisplayName("Action")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public Actions Action { get; set; } = Actions.grayscale;

        #endregion


        #region Response

        [Category("Response")]
        [DisplayName("Status code")]
        [ReadOnly(true)]
        public HttpStatusCode StatusCode { get; set; }

        [Category("Response")]
        [DisplayName("Body")]
        [ReadOnly(true)]
        public Image ResponseImage { get; set; }

        #endregion

        private List<string> possibleRoutes { get; set; }
    }
}