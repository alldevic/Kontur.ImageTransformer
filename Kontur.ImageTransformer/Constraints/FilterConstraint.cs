using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace Kontur.ImageTransformer.Constraints
{
    public class FilterConstraint : IHttpRouteConstraint
    {
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