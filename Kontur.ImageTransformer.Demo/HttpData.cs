using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using Kontur.ImageTransformer.Demo.Properties;

namespace Kontur.ImageTransformer.Demo
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
        public Rectangle Coords { get; set; } = new Rectangle(0, 0, 100, 100);

        [Category("Request")]
        [ReadOnly(true)]
        [DisplayName("Body")]
        public Image RequestImage { get; set; } = new Bitmap(Resources.zebra);

        [Category("Request")]
        [DisplayName("Action")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public Actions Action { get; set; } = Actions.Grayscale;

        #endregion


        #region Response

        [Category("Response")]
        [DisplayName("Status code")]
        [ReadOnly(true)]
        public HttpStatusCode StatusCode { get; set; }

        [Category("Response")]
        [DisplayName("Body")]
        [ReadOnly(true)]
        public Image ResponseImage { get; set; } = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

        #endregion


        public string Route()
        {
            var str = Action.GetDescription();
            return Action == Actions.Threshold
                ? $"process/{str}({ThresholdLevel})/{Coords.X},{Coords.Y},{Coords.Width},{Coords.Height}/"
                : $"process/{str}/{Coords.X},{Coords.Y},{Coords.Width},{Coords.Height}/";
        }
    }
}