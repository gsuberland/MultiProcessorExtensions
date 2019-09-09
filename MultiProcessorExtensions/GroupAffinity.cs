using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class GroupAffinity
    {
        public UIntPtr Mask { get; private set; }
        public UInt16 Group { get; private set; }

        internal GroupAffinity(UIntPtr mask, UInt16 group)
        {
            this.Mask = mask;
            this.Group = group;
        }

        internal GroupAffinity(GROUP_AFFINITY affinity)
        {
            this.Mask = affinity.Mask;
            this.Group = affinity.Group;
        }
    }
}
