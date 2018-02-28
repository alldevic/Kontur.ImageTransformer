using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Kontur.ImageTransformer.Constraints
{
    /// <inheritdoc />
    /// <summary>
    /// Constrains a route parameter to represent only Coordinates values. Coordinates it's a rectangle, where (x,y) is
    /// top left corner of rectangle, w - width, h - height.
    /// </summary>
    public class CoordsConstraint : IHttpRouteConstraint
    {
        /// <inheritdoc />
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (!values.TryGetValue(parameterName, out var value) || value == null)
            {
                return false;
            }

            if (!(value is string stringValue))
            {
                return false;
            }

            var tmp = stringValue.Split(',');
            if (tmp.Length != 4)
            {
                return false;
            }

            if (long.TryParse(tmp[0], out var x) && x >= -2147483648 && x <= 2147483648 &&
                long.TryParse(tmp[1], out var y) && y >= -2147483648 && y <= 2147483648 &&
                long.TryParse(tmp[2], out var w) && w >= -2147483648 && w <= 2147483648 &&
                long.TryParse(tmp[3], out var h) && h >= -2147483648 && h <= 2147483648)
            {
                return true;
            }

            return false;
        }
    }
}