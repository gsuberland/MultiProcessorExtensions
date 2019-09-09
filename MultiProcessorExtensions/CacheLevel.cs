using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Enumeration specifying the level of a processor cache.
    /// </summary>
    public enum CacheLevel : byte
    {
        L1 = 1,
        L2 = 2,
        L3 = 3
    }
}
