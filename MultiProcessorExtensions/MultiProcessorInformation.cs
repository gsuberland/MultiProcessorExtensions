using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MultiProcessorExtensions
{
    public static class MultiProcessorInformation
    {
        /// <summary>
        /// Generic method for calling GetLogicalProcessorInformationEx that returns the correct structure type.
        /// </summary>
        /// <typeparam name="T">The structure type that should be returned. Must match the relationship type.</typeparam>
        /// <param name="relationshipType">The relationship type to call GetLogicalProcessorInformationEx with.</param>
        /// <returns></returns>
        private static T[] GetLogicalProcessorInformationEx<T>(LOGICAL_PROCESSOR_RELATIONSHIP relationshipType) where T : struct
        {
            // validate that the lookup type and class parameter matches properly
            var typeClassLooukp = new Dictionary<LOGICAL_PROCESSOR_RELATIONSHIP, Type>
            {
                { LOGICAL_PROCESSOR_RELATIONSHIP.ProcessorCore, typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_PROCESSOR) },
                { LOGICAL_PROCESSOR_RELATIONSHIP.ProcessorPackage, typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_PROCESSOR) },
                { LOGICAL_PROCESSOR_RELATIONSHIP.NumaNode, typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_NUMA_NODE) },
                { LOGICAL_PROCESSOR_RELATIONSHIP.Cache, typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_CACHE) },
                { LOGICAL_PROCESSOR_RELATIONSHIP.Group, typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_GROUP) },
            };
            if (typeClassLooukp[relationshipType] != typeof(T))
            {
                throw new InvalidCastException($"Generic type {typeof(T).Name} passed does not match the relationship type {relationshipType}.");
            }

            // find the length required for the buffer
            uint requiredLength = 0;
            bool success = NativeMethods.GetLogicalProcessorInformationEx(relationshipType, IntPtr.Zero, ref requiredLength);
            if (!success && Marshal.GetLastWin32Error() != 122 /* ERROR_INSUFFICIENT_BUFFER */)
            {
                throw new InvalidOperationException($"The call to GetLogicalProcessorInformationEx failed. Last error: {Marshal.GetLastWin32Error()}");
            }

            // pad allocation up to the nearest 64-bit boundary
            requiredLength += 8 - (requiredLength % 8);

            if (requiredLength > int.MaxValue)
            {
                throw new InvalidOperationException($"The call to GetLogicalProcessorInformationEx returned a required data size ({requiredLength}) that exceeded the maximum signed integer value.");
            }

            IntPtr memProcInfo = Marshal.AllocHGlobal((int)requiredLength);
            try
            {
                uint bufferLength = requiredLength;
                success = NativeMethods.GetLogicalProcessorInformationEx(relationshipType, memProcInfo, ref bufferLength);
                if (!success)
                {
                    throw new InvalidOperationException($"The call to GetLogicalProcessorInformationEx failed. Last error: {Marshal.GetLastWin32Error()}");
                }

                uint structLength = (uint)Marshal.SizeOf<T>();

                if (bufferLength > requiredLength || bufferLength < structLength)
                {
                    throw new InvalidOperationException($"The call to GetLogicalProcessorInformationEx returned an invalid data length ({bufferLength}).");
                }

                uint arrayCount = bufferLength / structLength;
                var structArray = new T[arrayCount];
                IntPtr pointer = memProcInfo;
                for (int i = 0; i < arrayCount; i++)
                {
                    structArray[i] = Marshal.PtrToStructure<T>(pointer);
                    pointer += (int)structLength;
                }

                return structArray;
            }
            finally
            {
                Marshal.FreeHGlobal(memProcInfo);
            }
        }

        public static ProcessorCacheInfo[] GetCacheInfo()
        {
            return GetLogicalProcessorInformationEx<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_CACHE>(LOGICAL_PROCESSOR_RELATIONSHIP.Cache)
                .Select(ci => new ProcessorCacheInfo(ci.Cache)).ToArray();
        }

        public static ProcessorCoreInfo[] GetProcessorCoreInfo()
        {
            return GetLogicalProcessorInformationEx<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_PROCESSOR>(LOGICAL_PROCESSOR_RELATIONSHIP.ProcessorCore)
                .Select(ci => new ProcessorCoreInfo(ci.Processor)).ToArray();
        }

        public static ProcessorPackageInfo[] GetProcessorPackageInfo()
        {
            return GetLogicalProcessorInformationEx<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_PROCESSOR>(LOGICAL_PROCESSOR_RELATIONSHIP.ProcessorPackage)
                .Select(ci => new ProcessorPackageInfo(ci.Processor)).ToArray();
        }

        public static NumaNodeInfo[] GetNumaNodeInfo()
        {
            return GetLogicalProcessorInformationEx<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_NUMA_NODE>(LOGICAL_PROCESSOR_RELATIONSHIP.NumaNode)
                .Select(ci => new NumaNodeInfo(ci.NumaNode)).ToArray();
        }

        public static ProcessorGroupsInfo[] GetGroupsInfo()
        {
            return GetLogicalProcessorInformationEx<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_GROUP>(LOGICAL_PROCESSOR_RELATIONSHIP.Group)
                .Select(ci => new ProcessorGroupsInfo(ci.Group)).ToArray();
        }
    }
}
