using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing.Constraints;
using System.Web.Http.SelfHost;
using Kontur.ImageTransformer.Formatters;
using Kontur.ImageTransformer.Handlers;
using Kontur.ImageTransformer.ImageFilters;
using Kontur.ImageTransformer.Selectors;
using NLog;
using ThrottlingSuite.Core;
using ThrottlingSuite.Http.Handlers;

namespace Kontur.ImageTransformer
{
    public static class Configuration
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public const string Address = "http://localhost:8080";

        private static readonly int MaxPerSecond = Environment.ProcessorCount * 1200;

        private const int MaxReceivedSize = 102400;


        public static void SetConfiguration(HttpSelfHostConfiguration config)
        {
            config.MaxConcurrentRequests = 3000 * Environment.ProcessorCount;
            config.MaxReceivedMessageSize = MaxReceivedSize;
            config.MaxBufferSize = MaxReceivedSize;
            config.ReceiveTimeout = TimeSpan.FromMilliseconds(100);
            config.SendTimeout = TimeSpan.FromMilliseconds(200);
            Logger.Trace("Set main limits");

            config.Services.Replace(typeof(IHttpControllerSelector), new Http404DefaultSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new Http404ActionSelector());

            config.Formatters.Add(new BitmapWriteFormatter());
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            Logger.Trace("Configured selectors and formatters");

            ConfigureMessageHandlers(config);
            Logger.Trace("Configured throttling policy");
            ConfigureRoutes(config);
            Logger.Trace("Configured routes");
            PrecalcInit();
            Logger.Trace("Configuration done");
        }

        private static void ConfigureMessageHandlers(HttpSelfHostConfiguration config)
        {
            config.MessageHandlers.Add(new PostOnlyHandler());
            config.MessageHandlers.Add(new ThrottlingHandler(new ThrottlingControllerSuite(new ThrottlingConfiguration
            {
                ConcurrencyModel = ConcurrencyModel.Pessimistic,
                Enabled = true,
                LogOnly = false,
                SignatureBuilderParams =
                {
                    IgnoreAllQueryStringParameters = true,
                    IgnoreClientIpAddress = true,
                    EnableClientTracking = false,
                    UseInstanceUrl = true
                }
            }, new List<IThrottlingControllerInstance>(new[]
            {
                ThrottlingControllerInstance
                    .Create<LinearThrottlingController>("api", 1000, MaxPerSecond).IncludeInScope("process")
            }))));
        }

        private static void ConfigureRoutes(HttpSelfHostConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // POST /process/{filter}/{x},{y},{w},{h}
            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{action}/{x},{y},{w},{h}",
                constraints: new
                {
                    x = new IntRouteConstraint(),
                    y = new IntRouteConstraint(),
                    w = new IntRouteConstraint(),
                    h = new IntRouteConstraint(),
                },
                defaults: null);

            // Response 400 if no route matched
            config.Routes.MapHttpRoute("BadRequestApi", "{*url}",
                new {controller = "BadRequest", action = "Handle404"});
        }

        private static void PrecalcInit()
        {
            Logger.Trace($"Init Sepia precalc: [0xFFFFFF]={Precalc.SepiaInt[0xFFFFFF]}");
            Logger.Trace($"Init Grayscale  precalc: [0xFFFFFF]={Precalc.GrayInt[0xFFFFFF]}");
        }
    }
}