using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Provides information about a NUMA node in a processor group.
    /// </summary>
    public sealed class NumaNodeInfo
    {
        /// <summary>
        /// The number of the NUMA node.
        /// </summary>
        public uint NodeNumber { get; private set; }

        /// <summary>
        /// The group affinity associated with this NUMA node.
        /// </summary>
        public GroupAffinity Affinity { get; private set; }

        internal NumaNodeInfo(NUMA_NODE_RELATIONSHIP node)
        {
            this.NodeNumber = node.NodeNumber;
            this.Affinity = new GroupAffinity(node.GroupMask);
        }
    }
}
