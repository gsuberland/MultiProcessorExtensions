using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class ProcessorCoreInfo
    {
        private const byte LTP_PC_SMT = 0x01;

        public bool IsSMT { get; private set; }
        public byte EfficiencyClass { get; private set; }
        public GroupAffinity[] Affinities { get; private set; }

        internal ProcessorCoreInfo(PROCESSOR_RELATIONSHIP core)
        {
            this.IsSMT = (core.Flags & LTP_PC_SMT) == LTP_PC_SMT;
            this.EfficiencyClass = core.EfficiencyClass;
            this.Affinities = new GroupAffinity[core.GroupCount];
            for (int i = 0; i < core.GroupCount; i++)
            {
                this.Affinities[i] = new GroupAffinity(core.GroupMask[i]);
            }
        }
    }
}
