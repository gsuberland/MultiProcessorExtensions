using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MultiProcessorExtensions
{
    public static class ProcessExtensions
    {
        /// <summary>
        /// Retrieves the processor group affinity of the specified process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns>An array of processor group numbers. A group number is included in the array if a thread in the process is assigned to a processor in the group.</returns>
        public static ushort[] GetGroupAffinity(this Process process)
        {
            var procHandle = process.Handle;

            ushort arraySize = 0;
            ushort[] affinities;

            bool success = false;
            const int blown = 100;
            int fuse = 0;

            /* Attempt to call GetProcessGroupAffinity until the buffer size is sufficient.
             * This handles a race condition whereby the group count changes between an initial call to GetProcessGroupAffinity
             * to get the required buffer length, and a second call to fill the buffer.
             */
            do
            {
                affinities = new ushort[arraySize];
                if (!NativeMethods.GetProcessGroupAffinity(procHandle, ref arraySize, affinities))
                {
                    var lastError = Marshal.GetLastWin32Error();
                    if (lastError != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
                    {
                        throw new InvalidOperationException($"GetProcessGroupAffinity call failed. GetLastError: {lastError}");
                    }
                    if (++fuse == blown)
                    {
                        throw new TimeoutException("GetProcessGroupAffinity call did not return a valid buffer size.");
                    }
                }
                else
                {
                    success = true;
                }
            }
            while (!success);
            return affinities;
        }
    }
}
