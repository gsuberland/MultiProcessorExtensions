using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Provides information about a processor cache in a processor group.
    /// </summary>
    public sealed class ProcessorCacheInfo
    {
        /// <summary>
        /// The cache level.
        /// </summary>
        public CacheLevel Level { get; private set; }

        /// <summary>
        /// The cache associativity. A value of 0xFF indicates that the cache is fully associative.
        /// </summary>
        public byte Associativity { get; private set; }

        /// <summary>
        /// The cache line size, in bytes.
        /// </summary>
        public UInt16 LineSize { get; private set; }

        /// <summary>
        /// The cache size, in bytes.
        /// </summary>
        public UInt32 CacheSize { get; private set; }

        /// <summary>
        /// The cache type.
        /// </summary>
        public CacheType Type { get; private set; }

        /// <summary>
        /// The group affinity associated with this cache.
        /// </summary>
        public GroupAffinity Affinity { get; private set; }
        
        internal ProcessorCacheInfo(CACHE_RELATIONSHIP cache)
        {
            this.Level = (CacheLevel)cache.Level;
            this.Associativity = cache.Associativity;
            this.LineSize = cache.LineSize;
            this.CacheSize = cache.CacheSize;
            this.Type = (CacheType)cache.Type;
            this.Affinity = new GroupAffinity(cache.GroupMask.Mask, cache.GroupMask.Group);
        }
    }
}
