using System;
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
            logger.Trace("Hello world!");
        }
    }
}