using System;
using System.Collections.Generic;
using System.Text;

namespace MultiProcessorExtensions
{
    /// <summary>
    /// Provides information about an active processor group on the system.
    /// </summary>
    public sealed class ProcessorGroupInfo
    {
        /// <summary>
        /// The maximum number of processors in the group.
        /// </summary>
        public byte MaximumProcessorCount { get; private set; }

        /// <summary>
        /// The number of active processors in the group.
        /// </summary>
        public byte ActiveProcessorCount { get; private set; }

        /// <summary>
        /// A bitmap that specifies the affinity for zero or more active processors within the group.
        /// </summary>
        public UIntPtr ActiveProcessorMask { get; private set; }

        internal ProcessorGroupInfo(PROCESSOR_GROUP_INFO group)
        {
            this.MaximumProcessorCount = group.MaximumProcessorCount;
            this.ActiveProcessorCount = group.ActiveProcessorCount;
            this.ActiveProcessorMask = group.Affinity;
        }

        /// <summary>
        /// Returns true if a processor with the given index is active in the group.
        /// </summary>
        /// <param name="maskIndex">The index of the processor. Must be greater than zero and less than the mask size (32 on 32-bit systems, 64 on 64-bit).</param>
        /// <returns></returns>
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
