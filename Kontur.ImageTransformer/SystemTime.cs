// Based on https://github.com/robertmircea/RateLimiters/blob/master/src/Bert.RateLimiters/SystemTime.cs

using System;

namespace Kontur.ImageTransformer
{
    public static class SystemTime
    {
        // Allow modification of "Today" for unit testing
        private static readonly Func<DateTime> SetCurrentTimeUtc = () => DateTime.UtcNow;
        private static readonly Func<DateTime> SetCurrentTime = () => DateTime.Now;
        private static readonly Func<int> SetTickCount = () => Environment.TickCount;

        public static DateTime UtcNow => SetCurrentTimeUtc();

        public static DateTime Now => SetCurrentTime();

        public static int EnvironmentTickCount => SetTickCount();
    }
}