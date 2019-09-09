using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Provides information about a processor core in a processor group.
    /// </summary>
    public sealed class ProcessorCoreInfo
    {
        private const byte LTP_PC_SMT = 0x01;

        /// <summary>
        /// This property is set to true if simultaneous multithreading (SMT) is enabled for this processor core.
        /// </summary>
        public bool IsSMT { get; private set; }

        /// <summary>
        /// Specifies the intrinsic tradeoff between performance and power for the applicable core. A core with a higher value for the efficiency class has intrinsically greater performance and less efficiency than a core with a lower value for the efficiency class. EfficiencyClass is only nonzero on systems with a heterogeneous set of cores.
        /// </summary>
        public byte EfficiencyClass { get; private set; }

        /// <summary>
        /// The group affinities associated with this processor core.
        /// </summary>
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
