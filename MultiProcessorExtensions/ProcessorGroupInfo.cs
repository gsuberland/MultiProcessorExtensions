using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    public sealed class ProcessorGroupInfo
    {
        public byte MaximumProcessorCount { get; private set; }
        public byte ActiveProcessorCount { get; private set; }
        public UIntPtr ActiveProcessorMask { get; private set; }

        internal ProcessorGroupInfo(PROCESSOR_GROUP_INFO group)
        {
            this.MaximumProcessorCount = group.MaximumProcessorCount;
            this.ActiveProcessorCount = group.ActiveProcessorCount;
            this.ActiveProcessorMask = group.Affinity;
        }

        public bool IsProcessorActive(int maskIndex)
        {
            // check that the given index is a valid bit offset in the mask
            int totalMaskBits = (UIntPtr.Size * 8) - 1;
            if (maskIndex < 0 || maskIndex > totalMaskBits)
            {
                throw new ArgumentOutOfRangeException(nameof(maskIndex), $"Mask index was not in the range 0 to {totalMaskBits}.");
            }

            /*
             * The documentation says nothing about the bits in the mask outside of the active processor range, so we must assume
             * that they are undefined.
             */
            if (maskIndex > ActiveProcessorCount - 1)
            {
                return false;
            }

            // return true if the bit is set in the mask
            return (ActiveProcessorMask.ToUInt64() & (1u << maskIndex)) != 0;
        }
    }
}
