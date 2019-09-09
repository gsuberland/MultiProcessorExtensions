using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Provides information about a processor package in a processor group.
    /// </summary>
    public sealed class ProcessorPackageInfo
    {
        /// <summary>
        /// The group affinities associated with this processor package.
        /// </summary>
        public GroupAffinity[] Affinities { get; private set; }

        internal ProcessorPackageInfo(PROCESSOR_RELATIONSHIP core)
        {
            this.Affinities = new GroupAffinity[core.GroupCount];
            for (int i = 0; i < core.GroupCount; i++)
            {
                this.Affinities[i] = new GroupAffinity(core.GroupMask[i]);
            }
        }
    }
}
