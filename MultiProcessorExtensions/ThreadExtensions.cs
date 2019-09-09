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
        /// <param name="thread">A ProcessThread object describing the thread.</param>
        /// <returns>Returns a GroupAffinity object which contains information about the group affinity of the thread.</returns>
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

        /// <summary>
        /// Sets the processor group affinity for the specified thread.
        /// </summary>
        /// <param name="thread">A ProcessThread object describing the thread.</param>
        /// <param name="groupNumber">The processor group number.</param>
        /// <param name="affinityMask">A bitmap that specifies the affinity for zero or more processors within the specified group.</param>
        public static void SetProcessorGroup(this ProcessThread thread, ushort groupNumber, UIntPtr affinityMask)
        {
            SetProcessorGroup(thread, new GroupAffinity(groupNumber, affinityMask));
        }

        /// <summary>
        /// Sets the processor group affinity for the specified thread.
        /// </summary>
        /// <param name="thread">A ProcessThread object describing the thread.</param>
        /// <param name="group">A GroupAffinity object that specifies the processor group.</param>
        public static void SetProcessorGroup(this ProcessThread thread, GroupAffinity group)
        {
            IntPtr hThread = NativeMethods.OpenThread(NativeMethods.ThreadAccess.SET_INFORMATION | NativeMethods.ThreadAccess.QUERY_INFORMATION, false, thread.Id);
            if (hThread == IntPtr.Zero)
            {
                throw new InvalidOperationException($"OpenThread call failed. GetLastError: {Marshal.GetLastWin32Error()}");
            }
            try
            {
                var newAffinity = new GROUP_AFFINITY
                {
                    Group = group.Group,
                    Mask = group.Mask
                };
                var previousAffinity = new GROUP_AFFINITY(); ;
                if (!NativeMethods.SetThreadGroupAffinity(hThread, ref newAffinity, ref previousAffinity))
                {
                    throw new InvalidOperationException($"SetThreadGroupAffinity call failed. GetLastError: {Marshal.GetLastWin32Error()}");
                }
            }
            finally
            {
                NativeMethods.CloseHandle(hThread);
            }
        }
    }
}
