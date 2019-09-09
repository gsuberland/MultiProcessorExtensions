using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MultiProcessorExtensions
{
    internal static class NativeMethods
    {
        [DllImport("kernel32", SetLastError=true)]
        public static extern bool GetLogicalProcessorInformationEx(
            LOGICAL_PROCESSOR_RELATIONSHIP relationshipType,
            IntPtr buffer,
            ref UInt32 returnedLength
        );
    }
}
