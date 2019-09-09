using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class ProcessorGroupsInfo
    {
        public UInt16 MaximumGroupCount { get; private set; }
        public UInt16 ActiveGroupCount { get; private set; }
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
