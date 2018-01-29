using System;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Kontur.ImageTransformer
{
    public class HandleRequest
    {
        private string Name { get; } = "Client1";

        private int Seconds { get; } = 100;

        public async Task<bool> IsValidRequest(HttpRequestMessage requestMessage)
        {
            var allowExecute = false;
            await Task.Factory.StartNew(() =>
            {
                var key = $"{Name}-{GetClientIp(requestMessage)}";
                if (HttpRuntime.Cache[key] == null)
                {
                    HttpRuntime.Cache.Add(key, true, null, DateTime.Now.AddMilliseconds(Seconds),
                        Cache.NoSlidingExpiration, CacheItemPriority.Low, null);

                    allowExecute = true;
                }
            });


            return allowExecute;
        }

        private static string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                var prop = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }

            return null;
        }
    }
}