using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Kontur.ImageTransformer.Constraints
{
    public class TransformConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (!values.TryGetValue(parameterName, out var value) || value == null)
            {
                return false;
            }

            var stringValue = value as string;
            switch (stringValue)
            {
                case null:
                    return false;
                case "rotate-cw":
                    values[parameterName] = RotateFlipType.Rotate90FlipNone;
                    return true;
                case "rotate-ccw":
                    values[parameterName] = RotateFlipType.Rotate270FlipNone;
                    return true;
                case "flip-h":
                    values[parameterName] = RotateFlipType.RotateNoneFlipX;
                    return true;
                case "flip-v":
                    values[parameterName] = RotateFlipType.RotateNoneFlipY;
                    return true;
            }

            return false;
        }
    }
}