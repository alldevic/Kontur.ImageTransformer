// Based on https://github.com/robertmircea/RateLimiters/blob/master/src/Bert.RateLimiters/SystemTime.cs

using System;

namespace Kontur.ImageTransformer
{
    public static class SystemTime
    {
        // Allow modification of "Today" for unit testing
        public static Func<DateTime> SetCurrentTimeUtc = () => DateTime.UtcNow;
        public static Func<DateTime> SetCurrentTime = () => DateTime.Now;
        public static Func<int> SetTickCount = () => Environment.TickCount;

        public static DateTime UtcNow => SetCurrentTimeUtc();

        public static DateTime Now => SetCurrentTime();

        public static int EnvironmentTickCount => SetTickCount();
    }
}