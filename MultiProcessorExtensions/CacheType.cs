using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Enumeration specifying the type of a processor cache.
    /// </summary>
    public enum CacheType
    {
        Unified,
        Instruction,
        Data,
        Trace
    }
}
