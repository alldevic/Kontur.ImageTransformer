// Based on https://github.com/robertmircea/RateLimiters/blob/master/src/Bert.RateLimiters.Tests/FixedTokenBucketTests.cs

using System;
using System.Threading;
using NUnit.Framework;

namespace Kontur.ImageTransformer.Tests
{
    public class TokenBucketTests
    {
        private TokenBucket _bucket;
        public const long MaxTokens = 10;
        public const long RefillInterval = 10;
        public const long NLessThanMax = 2;
        public const long NGreaterThanMax = 12;
        private const int Cumulative = 2;

        [SetUp]
        public void SetUp()
        {
            _bucket = new TokenBucket(MaxTokens, 10000);
        }

        [Test]
        public void ShouldThrottle_WhenCalledWithNTokensLessThanMax_ReturnsFalse()
        {
            var shouldThrottle = _bucket.ShouldThrottle(NLessThanMax, out _);

            Assert.That(shouldThrottle, Is.False);
            Assert.That(_bucket.CurrentTokenCount, Is.EqualTo(MaxTokens - NLessThanMax));
        }

        [Test]
        public void ShouldThrottle_WhenCalledWithNTokensGreaterThanMax_ReturnsTrue()
        {
            var shouldThrottle = _bucket.ShouldThrottle(NGreaterThanMax, out var waitTime);

            Assert.That(shouldThrottle, Is.True);
            Assert.That(waitTime, Is.EqualTo(TimeSpan.FromMilliseconds(RefillInterval * 1000)));
            Assert.That(_bucket.CurrentTokenCount, Is.EqualTo(MaxTokens));
        }


        [Test]
        public void ShouldThrottle_WhenCalledCumulativeNTimesIsLessThanMaxTokens_ReturnsFalse()
        {
            for (var i = 0; i < Cumulative; i++)
            {
                Assert.That(_bucket.ShouldThrottle(NLessThanMax, out var waitTime), Is.False);
                Assert.That(waitTime, Is.EqualTo(TimeSpan.Zero));
            }

            var tokens = _bucket.CurrentTokenCount;

            Assert.That(tokens, Is.EqualTo(MaxTokens - (Cumulative * NLessThanMax)));
        }


        [Test]
        public void ShouldThrottle_WhenCalledCumulativeNTimesIsGreaterThanMaxTokens_ReturnsTrue()
        {
            for (var i = 0; i < Cumulative; i++)
            {
                Assert.That(_bucket.ShouldThrottle(NGreaterThanMax), Is.True);
            }

            var tokens = _bucket.CurrentTokenCount;

            Assert.That(tokens, Is.EqualTo(MaxTokens));
        }


        [Test]
        public void ShouldThrottle_WhenCalledWithNLessThanMaxSleepNLessThanMax_ReturnsFalse()
        {
            SystemTime.SetCurrentTimeUtc = () => new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);
            var virtualNow = SystemTime.UtcNow;

            var before = _bucket.ShouldThrottle(NLessThanMax);
            var tokensBefore = _bucket.CurrentTokenCount;
            Assert.That(before, Is.False);
            Assert.That(tokensBefore, Is.EqualTo(MaxTokens - NLessThanMax));

            SystemTime.SetCurrentTimeUtc = () => virtualNow.AddSeconds(RefillInterval);

            var after = _bucket.ShouldThrottle(NLessThanMax);
            var tokensAfter = _bucket.CurrentTokenCount;
            Assert.That(after, Is.False);
            Assert.That(tokensAfter, Is.EqualTo(MaxTokens - NLessThanMax));
        }

        [Test]
        public void ShouldThrottle_WhenCalledWithNGreaterThanMaxSleepNGreaterThanMax_ReturnsTrue()
        {
            SystemTime.SetCurrentTimeUtc = () => new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);
            var virtualNow = SystemTime.UtcNow;

            var before = _bucket.ShouldThrottle(NGreaterThanMax, out var waitTime);
            var tokensBefore = _bucket.CurrentTokenCount;
            Assert.That(waitTime, Is.EqualTo(TimeSpan.FromSeconds(RefillInterval)));
            Assert.That(before, Is.True);
            Assert.That(tokensBefore, Is.EqualTo(MaxTokens));

            SystemTime.SetCurrentTimeUtc = () => virtualNow.AddSeconds(RefillInterval + 1);

            var after = _bucket.ShouldThrottle(NGreaterThanMax, out waitTime);
            var tokensAfter = _bucket.CurrentTokenCount;
            Assert.That(after, Is.True);
            Assert.That(waitTime, Is.EqualTo(TimeSpan.FromSeconds(RefillInterval)));
            Assert.That(tokensAfter, Is.EqualTo(MaxTokens));
        }

        [Test]
        public void ShouldThrottle_WhenThrottle_WaitTimeIsDynamicallyCalculated()
        {
            var virtualTime = new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);

            for (var i = 0; i < 3; i++)
            {
                var closureI = i;
                SystemTime.SetCurrentTimeUtc = () => virtualTime.AddSeconds(closureI * 3);
                _bucket.ShouldThrottle(NGreaterThanMax, out var waitTime);
                Assert.That(waitTime, Is.EqualTo(TimeSpan.FromSeconds(10 - i * 3)));
            }
        }


        [Test]
        public void ShouldThrottle_WhenCalledWithNLessThanMaxSleepCumulativeNLessThanMax()
        {
            SystemTime.SetCurrentTimeUtc = () => new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);
            var virtualNow = SystemTime.UtcNow;

            long sum = 0;
            for (var i = 0; i < Cumulative; i++)
            {
                Assert.That(_bucket.ShouldThrottle(NLessThanMax), Is.False);
                sum += NLessThanMax;
            }

            var tokensBefore = _bucket.CurrentTokenCount;
            Assert.That(tokensBefore, Is.EqualTo(MaxTokens - sum));

            SystemTime.SetCurrentTimeUtc = () => virtualNow.AddSeconds(RefillInterval);

            for (var i = 0; i < Cumulative; i++)
            {
                Assert.That(_bucket.ShouldThrottle(NLessThanMax), Is.False);
            }

            var tokensAfter = _bucket.CurrentTokenCount;
            Assert.That(tokensAfter, Is.EqualTo(MaxTokens - sum));
        }

        [Test]
        public void ShouldThrottle_WhenCalledWithCumulativeNLessThanMaxSleepCumulativeNGreaterThanMax()
        {
            SystemTime.SetCurrentTimeUtc = () => new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);
            var virtualNow = SystemTime.UtcNow;

            long sum = 0;
            for (var i = 0; i < Cumulative; i++)
            {
                Assert.That(_bucket.ShouldThrottle(NLessThanMax), Is.False);
                sum += NLessThanMax;
            }

            var tokensBefore = _bucket.CurrentTokenCount;
            Assert.That(tokensBefore, Is.EqualTo(MaxTokens - sum));

            SystemTime.SetCurrentTimeUtc = () => virtualNow.AddSeconds(RefillInterval);

            for (var i = 0; i < 3 * Cumulative; i++)
            {
                _bucket.ShouldThrottle(NLessThanMax);
            }

            var after = _bucket.ShouldThrottle(NLessThanMax);
            var tokensAfter = _bucket.CurrentTokenCount;

            Assert.That(after, Is.True);
            Assert.That(tokensAfter, Is.LessThan(NLessThanMax));
        }

        [Test]
        public void ShouldThrottle_WhenCalledWithCumulativeNGreaterThanMaxSleepCumulativeNLessThanMax()
        {
            SystemTime.SetCurrentTimeUtc = () => new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);
            var virtualNow = SystemTime.UtcNow;

            for (var i = 0; i < 3 * Cumulative; i++)
                _bucket.ShouldThrottle(NLessThanMax);

            var before = _bucket.ShouldThrottle(NLessThanMax);
            var tokensBefore = _bucket.CurrentTokenCount;

            Assert.That(before, Is.True);
            Assert.That(tokensBefore, Is.LessThan(NLessThanMax));

            SystemTime.SetCurrentTimeUtc = () => virtualNow.AddSeconds(RefillInterval);

            long sum = 0;
            for (var i = 0; i < Cumulative; i++)
            {
                Assert.That(_bucket.ShouldThrottle(NLessThanMax), Is.False);
                sum += NLessThanMax;
            }

            var tokensAfter = _bucket.CurrentTokenCount;
            Assert.That(tokensAfter, Is.EqualTo(MaxTokens - sum));
        }


        [Test]
        public void ShouldThrottle_WhenCalledWithCumulativeNGreaterThanMaxSleepCumulativeNGreaterThanMax()
        {
            SystemTime.SetCurrentTimeUtc = () => new DateTime(2014, 2, 27, 0, 0, 0, DateTimeKind.Utc);
            var virtualNow = SystemTime.UtcNow;

            for (var i = 0; i < 3 * Cumulative; i++)
                _bucket.ShouldThrottle(NLessThanMax);

            var before = _bucket.ShouldThrottle(NLessThanMax);
            var tokensBefore = _bucket.CurrentTokenCount;

            Assert.That(before, Is.True);
            Assert.That(tokensBefore, Is.LessThan(NLessThanMax));

            SystemTime.SetCurrentTimeUtc = () => virtualNow.AddSeconds(RefillInterval);

            for (var i = 0; i < 3 * Cumulative; i++)
            {
                _bucket.ShouldThrottle(NLessThanMax);
            }

            var after = _bucket.ShouldThrottle(NLessThanMax);
            var tokensAfter = _bucket.CurrentTokenCount;

            Assert.That(after, Is.True);
            Assert.That(tokensAfter, Is.LessThan(NLessThanMax));
        }

        [Test]
        public void ShouldThrottle_WhenThread1NLessThanMaxAndThread2NLessThanMax()
        {
            var t1 = new Thread(p =>
            {
                var throttle = _bucket.ShouldThrottle(NLessThanMax);
                Assert.That(throttle, Is.False);
            });

            var t2 = new Thread(p =>
            {
                var throttle = _bucket.ShouldThrottle(NLessThanMax);
                Assert.That(throttle, Is.False);
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Assert.That(_bucket.CurrentTokenCount, Is.EqualTo(MaxTokens - 2 * NLessThanMax));
        }

        [Test]
        public void ShouldThrottle_Thread1NGreaterThanMaxAndThread2NGreaterThanMax()
        {
            var shouldThrottle = _bucket.ShouldThrottle(NGreaterThanMax);
            Assert.That(shouldThrottle, Is.True);

            var t1 = new Thread(p =>
            {
                var throttle = _bucket.ShouldThrottle(NGreaterThanMax);
                Assert.That(throttle, Is.True);
            });

            var t2 = new Thread(p =>
            {
                var throttle = _bucket.ShouldThrottle(NGreaterThanMax);
                Assert.That(throttle, Is.True);
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Assert.That(_bucket.CurrentTokenCount, Is.EqualTo(MaxTokens));
        }
    }
}