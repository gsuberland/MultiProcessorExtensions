using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MultiProcessorExtensions
{
    public static class ThreadExtensions
    {
        /// <summary>
        /// Gets the processor group for the specified thread.
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static GroupAffinity GetProcessorGroup(this ProcessThread thread)
        {
            IntPtr hThread = NativeMethods.OpenThread(NativeMethods.ThreadAccess.QUERY_INFORMATION, false, thread.Id);
            if (hThread == IntPtr.Zero)
            {
                throw new InvalidOperationException($"OpenThread call failed. GetLastError: {Marshal.GetLastWin32Error()}");
            }
            try
            {
                var affinity = new GROUP_AFFINITY();
                if (!NativeMethods.GetThreadGroupAffinity(hThread, ref affinity))
                {
                    throw new InvalidOperationException($"GetThreadGroupAffinity call failed. GetLastError: {Marshal.GetLastWin32Error()}");
                }
                return new GroupAffinity(affinity);
            }
            finally
            {
                NativeMethods.CloseHandle(hThread);
            }
        }
    }
}
