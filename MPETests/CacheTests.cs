using System;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class CacheTests
    {
        [Fact]
        public void ProcessorCacheInfoReturnsData()
        {
            var caches = MultiProcessorInformation.GetCacheInfo();
            Assert.NotEmpty(caches);
        }

        [Fact]
        public void ProcessorCacheLevelsWithinRange()
        {
            var caches = MultiProcessorInformation.GetCacheInfo();
            foreach (var cache in caches)
            {
                Assert.True(Enum.IsDefined(typeof(CacheLevel), cache.Level));
            }
        }

        [Fact]
        public void ProcessorCacheTypesWithinRange()
        {
            var caches = MultiProcessorInformation.GetCacheInfo();
            foreach (var cache in caches)
            {
                Assert.True(Enum.IsDefined(typeof(CacheType), cache.Type));
            }
        }

        [Fact]
        public void ProcessorCacheSizeGreaterThanZero()
        {
            var caches = MultiProcessorInformation.GetCacheInfo();
            foreach (var cache in caches)
            {
                Assert.True(cache.CacheSize > 0);
            }
        }

        [Fact]
        public void ProcessorCacheLineSizeGreaterThanZero()
        {
            var caches = MultiProcessorInformation.GetCacheInfo();
            foreach (var cache in caches)
            {
                Assert.True(cache.LineSize > 0);
            }
        }

        [Fact]
        public void ProcessorCacheGroupMaskIsNotZero()
        {
            var caches = MultiProcessorInformation.GetCacheInfo();
            foreach (var cache in caches)
            {
                Assert.True(cache.Affinity.Mask != UIntPtr.Zero);
            }
        }
    }
}
