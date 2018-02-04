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
            var config = new HttpSelfHostConfiguration("http://localhost:8080")
            {
                MaxConcurrentRequests = 100,
                MaxReceivedMessageSize = int.MaxValue,
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