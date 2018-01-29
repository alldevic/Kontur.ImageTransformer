using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing.Constraints;
using System.Web.Http.SelfHost;
using Kontur.ImageTransformer.Handlers;
using Kontur.ImageTransformer.Selectors;
using NLog;

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
            var config = new HttpSelfHostConfiguration(address)
            {
                MaxConcurrentRequests = 1000,
                MaxReceivedMessageSize = 102400,
                MaxBufferSize = 102400,
            };
            Logger.Trace("Set base settings");

            RouteConfig(config);
            Logger.Trace("Set routes");

            config.MessageHandlers.Add(new PostOnlyHandler());
            Logger.Trace("Allow only POST requests");

            config.Services.Replace(typeof(IHttpControllerSelector), new Http404DefaultSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new Http404ActionSelector());
            Logger.Trace("Replace selectors");

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Logger.Info($"Server running at {address}. Press any key to exit");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Configure routes to process/filter/x,y,w,h/
        /// </summary>
        /// <param name="config">Configuration of server</param>
        private static void RouteConfig(HttpSelfHostConfiguration config)
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
    }
}