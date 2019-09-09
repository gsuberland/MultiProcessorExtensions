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

        public static void SetProcessorGroup(this ProcessThread thread, ushort groupNumber, UIntPtr affinityMask)
        {
            SetProcessorGroup(thread, new GroupAffinity(groupNumber, affinityMask));
        }

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
