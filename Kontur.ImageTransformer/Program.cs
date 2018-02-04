using System;
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
            Logger.Warn($"{typeof(Program).Assembly.GetName().Name} started");
            var config = new HttpSelfHostConfiguration(Configuration.Address);
            Configuration.SetConfiguration(config);


            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Logger.Warn($"Server running at {Configuration.Address}");
                Console.ReadLine();
            }

            Logger.Info($"{typeof(Program).Assembly.GetName().Name} stopped");
        }
    }
}