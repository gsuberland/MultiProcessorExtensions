using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class ProcessorPackageInfo
    {
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
