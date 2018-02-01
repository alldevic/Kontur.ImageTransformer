using System;
using System.Runtime;
using System.Web.Http.SelfHost;
using NLog;

namespace Kontur.ImageTransformer
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.Trace("App started");
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            Logger.Trace($"{GCSettings.LatencyMode.ToString()}");
            var config = new HttpSelfHostConfiguration(Configuration.Address);
            Configuration.SetConfiguration(config);
            Logger.Info("Server configured");


            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Logger.Info($"Server running at {Configuration.Address}");
                Console.WriteLine("Press ENTER for exit");
                do
                {
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
            }
        }
    }
}