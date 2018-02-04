using System.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.SelfHost;
using System.Web.Http.Tracing;
using Kontur.ImageTransformer.Handlers;
using Kontur.ImageTransformer.Helpers;
using Kontur.ImageTransformer.Selectors;
using NLog;
using ThrottlingSuite.Core;
using ThrottlingSuite.Http.Handlers;

namespace Kontur.ImageTransformer
{
    public static class Configuration
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static HttpSelfHostConfiguration SetConfiguration()
        {
            Logger.Info("Starting configuration");
            var appSettings = ConfigurationManager.AppSettings;
            var address = appSettings["Address"] ?? "http://localhost:8080";
            if (!int.TryParse(appSettings["MaxReceiveSize"], out var maxReceiveSize))
            {
                maxReceiveSize = 102400;
            }

            if (!int.TryParse(appSettings["MaxConcurrentRequests"], out var maxRequests))
            {
                maxRequests = 102400;
            }

            var config = new HttpSelfHostConfiguration(address)
            {
                MaxConcurrentRequests = maxRequests,
                MaxReceivedMessageSize = maxReceiveSize,
                MaxBufferSize = maxReceiveSize,
            };

            config.MessageHandlers.Add(new MainCheckHandler());
            config.MessageHandlers.Add(new ThrottlingHandler(new ThrottlingControllerSuite()));

            config.Services.Replace(typeof(IHttpControllerSelector), new Http404DefaultSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new Http404ActionSelector());
            config.Services.Replace(typeof(ITraceWriter), new NlogTraceWriter());
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());

            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("404-API", "{*url}", new {controller = "BadRequest", action = "Handle404"});

            Logger.Info("Configuration done");
            Logger.Info("Starting precalc");
            PrecalcInit();
            Logger.Trace("Precalc done");
            return config;
        }

        private static void PrecalcInit()
        {
            Logger.Trace($"Init Sepia precalc: [0xFFFFFF]={ImageFilters.SepiaUInt[0xFFFFFF]}");
            Logger.Trace($"Init Grayscale  precalc: [0xFFFFFF]={ImageFilters.GrayUInt[0xFFFFFF]}");
        }
    }
}