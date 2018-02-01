using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing.Constraints;
using System.Web.Http.SelfHost;
using Kontur.ImageTransformer.Handlers;
using Kontur.ImageTransformer.ImageFilters;
using Kontur.ImageTransformer.Selectors;
using NLog;
using ThrottlingSuite.Core;
using ThrottlingSuite.Http.Handlers;

namespace Kontur.ImageTransformer
{
    public class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.Trace("App started");


            const string address = "http://localhost:8080";
            var config = new HttpSelfHostConfiguration(address);
            ConfigureServer(config);

            Logger.Trace($"Sepia {Precalc.SepiaInt[0xFFFFFF]}");
            Logger.Trace($"Grayscale {Precalc.GrayInt[0xFFFFFF]}");

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Logger.Info($"Server running at {address}. Press any key to exit");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Configure server
        /// </summary>
        /// <param name="cfg">Configuration of server</param>
        private static void ConfigureServer(HttpSelfHostConfiguration cfg)
        {
            var maxPerSecond = Environment.ProcessorCount * 1200;

            cfg.MaxConcurrentRequests = 3000 * Environment.ProcessorCount;
            cfg.MaxReceivedMessageSize = 102400;
            cfg.MaxBufferSize = 102400;

            cfg.Services.Replace(typeof(IHttpControllerSelector), new Http404DefaultSelector(cfg));
            cfg.Services.Replace(typeof(IHttpActionSelector), new Http404ActionSelector());

            cfg.Formatters.Remove(cfg.Formatters.FormUrlEncodedFormatter);
            cfg.Formatters.Remove(cfg.Formatters.XmlFormatter);
            
            
            
            cfg.MessageHandlers.Add(new PostOnlyHandler());
            cfg.MessageHandlers.Add(new ThrottlingHandler(new ThrottlingControllerSuite(new ThrottlingConfiguration
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
                    .Create<LinearThrottlingController>("api", 1000, maxPerSecond).IncludeInScope("process")
            }))));

            cfg.MapHttpAttributeRoutes();

            // POST /process/{filter}/{x},{y},{w},{h}
            cfg.Routes.MapHttpRoute("DefaultApi", "{controller}/{action}/{x},{y},{w},{h}",
                constraints: new
                {
                    x = new IntRouteConstraint(),
                    y = new IntRouteConstraint(),
                    w = new IntRouteConstraint(),
                    h = new IntRouteConstraint(),
                },
                defaults: null);

            // Response 400 if no route matched
            cfg.Routes.MapHttpRoute("BadRequestApi", "{*url}", new {controller = "BadRequest", action = "Handle404"});
        }
    }
}