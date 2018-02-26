// Based on https://github.com/robertmircea/RateLimiters/blob/master/src/Bert.RateLimiters/TokenBucket.cs
// Based on https://github.com/robertmircea/RateLimiters/blob/master/src/Bert.RateLimiters/FixedTokenBucket.cs

using System;

namespace Kontur.ImageTransformer
{
    public class TokenBucket
    {
        private readonly long _bucketTokenCapacity;
        private static readonly object SyncRoot = new object();
        private readonly long _ticksRefillInterval;
        private long _nextRefillTime;

        //number of tokens in the bucket
        private long _tokens;

        public TokenBucket(long bucketTokenCapacity, long refillIntervalInMilliSeconds)
        {
            if (bucketTokenCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(bucketTokenCapacity),
                    "bucket token capacity can not be negative");
            
            if (refillIntervalInMilliSeconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(refillIntervalInMilliSeconds),
                    "Refill interval in milliseconds cannot be negative");

            _bucketTokenCapacity = bucketTokenCapacity;
            _ticksRefillInterval = TimeSpan.FromMilliseconds(refillIntervalInMilliSeconds).Ticks;
        }

        public bool ShouldThrottle(long n = 1) => ShouldThrottle(n, out _);

        public bool ShouldThrottle(long n, out TimeSpan waitTime)
        {
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Should be positive integer");

            lock (SyncRoot)
            {
                UpdateTokens();
                if (_tokens < n)
                {
                    var timeToIntervalEnd = _nextRefillTime - SystemTime.UtcNow.Ticks;
                    if (timeToIntervalEnd < 0) return ShouldThrottle(n, out waitTime);

                    waitTime = TimeSpan.FromTicks(timeToIntervalEnd);
                    return true;
                }

                _tokens -= n;

                waitTime = TimeSpan.Zero;
                return false;
            }
        }

        private void UpdateTokens()
        {
            var currentTime = SystemTime.UtcNow.Ticks;

            if (currentTime < _nextRefillTime) return;

            lock (SyncRoot)
            {
                _tokens = _bucketTokenCapacity;
            }

            _nextRefillTime = currentTime + _ticksRefillInterval;
        }

        public bool ShouldThrottle(out TimeSpan waitTime) => ShouldThrottle(1, out waitTime);

        public long CurrentTokenCount
        {
            get
            {
                lock (SyncRoot)
                {
                    UpdateTokens();
                    return _tokens;
                }
            }
        }
    }
}