using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MultiProcessorExtensions
{
    internal static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetLogicalProcessorInformationEx(
            LOGICAL_PROCESSOR_RELATIONSHIP relationshipType,
            IntPtr buffer,
            ref UInt32 returnedLength
        );

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool GetThreadGroupAffinity(
            IntPtr handle,
            ref GROUP_AFFINITY affinity
        );

        [Flags]
        internal enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenThread(
            ThreadAccess dwDesiredAccess,
            bool bInheritHandle,
            int dwThreadId
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}
