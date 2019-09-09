using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class ProcessorCacheInfo
    {
        public CacheLevel Level { get; private set; }
        public byte Associativity { get; private set; }
        public UInt16 LineSize { get; private set; }
        public UInt32 CacheSize { get; private set; }
        public CacheType Type { get; private set; }
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
