using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace Kontur.ImageTransformer.Constraints
{
    /// <inheritdoc />
    /// <summary>
    /// Constrains a route parameter to represent only ImageFilter values. It's a 'grayscale', 'sepia' or
    /// 'threshold(x)', where x in range [0;100]
    /// </summary>
    public class FilterConstraint : IHttpRouteConstraint
    {
        /// <inheritdoc />
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (!values.TryGetValue(parameterName, out var value) || value == null || !(value is string stringValue))
            {
                return false;
            }

            if (stringValue.Equals("grayscale") || stringValue.Equals("sepia"))
            {
                return true;
            }

            return Regex.IsMatch(stringValue, @"^threshold\(([0-9]|[1-9][0-9]|100)\)$",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        }
    }
}