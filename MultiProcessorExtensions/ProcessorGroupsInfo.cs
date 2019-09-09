using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Provides information about processor groups on the system.
    /// </summary>
    public sealed class ProcessorGroupsInfo
    {
        /// <summary>
        /// The maximum number of processor groups on the system.
        /// </summary>
        public UInt16 MaximumGroupCount { get; private set; }

        /// <summary>
        /// The number of active groups on the system.
        /// </summary>
        public UInt16 ActiveGroupCount { get; private set; }

        /// <summary>
        /// An array of ProcessorGroupInfo objects containing information about active groups on the system.
        /// </summary>
        public ProcessorGroupInfo[] Groups { get; private set; }

        internal ProcessorGroupsInfo(GROUP_RELATIONSHIP groups)
        {
            this.MaximumGroupCount = groups.MaximumGroupCount;
            this.ActiveGroupCount = groups.ActiveGroupCount;
            this.Groups = new ProcessorGroupInfo[this.ActiveGroupCount];
            for (int i = 0; i < this.ActiveGroupCount; i++)
            {
                this.Groups[i] = new ProcessorGroupInfo(groups.GroupInfo[i]);
            }
        }
    }
}
