using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class NumaNodeInfo
    {
        public uint NodeNumber { get; private set; }
        public GroupAffinity Affinity { get; private set; }

        internal NumaNodeInfo(NUMA_NODE_RELATIONSHIP node)
        {
            this.NodeNumber = node.NodeNumber;
            this.Affinity = new GroupAffinity(node.GroupMask);
        }
    }
}
