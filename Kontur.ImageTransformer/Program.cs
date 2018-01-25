using System;
using System.Web.Http.SelfHost;
using NLog;

namespace Kontur.ImageTransformer
{
    public class Program
    {
        public static bool Testcase = true;

        public static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        public static void Main(string[] args)
        {
            var address = "http://localhost:8080";
            var config = new HttpSelfHostConfiguration(address);
            
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                logger.Info($"Server running at {address}. Press any key to exit");
                Console.ReadKey();
            }
       
        }
    }
}