//TODO: test coverage
//TODO: +2**31

using System;
using System.Web.Http.SelfHost;
using NLog;

namespace Kontur.ImageTransformer
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The entry point for the application
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.Warn($"{typeof(Program).Assembly.GetName().Name} started");

            var config = Configuration.SetConfiguration();

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Logger.Warn($"Server running at {config.BaseAddress}");
                Console.ReadKey(true);
            }

            Logger.Warn($"{typeof(Program).Assembly.GetName().Name} stopped");
        }
    }
}