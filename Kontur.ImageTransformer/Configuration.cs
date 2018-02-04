using System;
using System.Collections.Generic;
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

        public const string Address = "http://localhost:8080";

        private static readonly int MaxPerSecond = Environment.ProcessorCount * 50;

        private const int MaxReceivedSize = 102400;


        public static void SetConfiguration(HttpSelfHostConfiguration config)
        {
            Logger.Info("Starting configuration");
            config.MaxConcurrentRequests = 100;
            config.MaxReceivedMessageSize = MaxReceivedSize;
            config.MaxBufferSize = MaxReceivedSize;
            config.ReceiveTimeout = TimeSpan.FromMilliseconds(100);
            config.SendTimeout = TimeSpan.FromMilliseconds(200);

            config.Services.Replace(typeof(IHttpControllerSelector), new Http404DefaultSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new Http404ActionSelector());
            config.Services.Replace(typeof(ITraceWriter), new NlogTraceWriter());
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());

            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            ConfigureMessageHandlers(config);
            ConfigureRoutes(config);

            Logger.Info("Configuration done");
            Logger.Info("Starting precalc");
            PrecalcInit();
            Logger.Trace("Precalc done");
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

            // Response 400 if no route matched
            config.Routes.MapHttpRoute("BadRequestApi", "{*url}",
                new {controller = "BadRequest", action = "Handle404"});
        }

        private static void PrecalcInit()
        {
            Logger.Trace($"Init Sepia precalc: [0xFFFFFF]={ImageFilters.SepiaInt[0xFFFFFF]}");
            Logger.Trace($"Init Grayscale  precalc: [0xFFFFFF]={ImageFilters.GrayInt[0xFFFFFF]}");
        }
    }
}