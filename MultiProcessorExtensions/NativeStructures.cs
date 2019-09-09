using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MultiProcessorExtensions
{
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winnt/ne-winnt-logical_processor_relationship"/>
    internal enum LOGICAL_PROCESSOR_RELATIONSHIP
    {
        ProcessorCore = 0,
        NumaNode = 1,
        Cache = 2,
        ProcessorPackage = 3,
        Group = 4,
        All = 0xFFFF
    }

    internal enum PROCESSOR_CACHE_TYPE
    {
        Unified,
        Instruction,
        Data,
        Trace
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_PROCESSOR
    {
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public UInt32 Size;
        public PROCESSOR_RELATIONSHIP Processor;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_NUMA_NODE
    {
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public UInt32 Size;
        public NUMA_NODE_RELATIONSHIP NumaNode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_GROUP
    {
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public UInt32 Size;
        public GROUP_RELATIONSHIP Group;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX_CACHE
    {
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public UInt32 Size;
        public CACHE_RELATIONSHIP Cache;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct PROCESSOR_RELATIONSHIP
    {
        public byte Flags;
        public byte EfficiencyClass;
        private UInt64 Reserved00_07;
        private UInt64 Reserved08_15;
        private UInt32 Reserved16_19;
        public UInt16 GroupCount;
        [MarshalAs(UnmanagedType.ByValArray)]
        public GROUP_AFFINITY[] GroupMask;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct GROUP_AFFINITY
    {
        public UIntPtr Mask;
        public UInt16 Group;
        private UInt16 Reserved0;
        private UInt16 Reserved1;
        private UInt16 Reserved2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct NUMA_NODE_RELATIONSHIP
    {
        public UInt32 NodeNumber;
        private UInt64 Reserved_00_07;
        private UInt64 Reserved_08_15;
        private UInt32 Reserved_16_19;
        public GROUP_AFFINITY GroupMask;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct GROUP_RELATIONSHIP
    {
        public UInt16 MaximumGroupCount;
        public UInt16 ActiveGroupCount;
        private UInt64 Reserved_00_07;
        private UInt64 Reserved_08_15;
        private UInt32 Reserved_16_19;
        [MarshalAs(UnmanagedType.ByValArray)]
        public PROCESSOR_GROUP_INFO[] GroupInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct CACHE_RELATIONSHIP
    {
        public byte Level;
        public byte Associativity;
        public UInt16 LineSize;
        public UInt32 CacheSize;
        public PROCESSOR_CACHE_TYPE Type;
        private UInt64 Reserved_00_07;
        private UInt64 Reserved_08_15;
        private UInt32 Reserved_16_19;
        public GROUP_AFFINITY GroupMask;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct PROCESSOR_GROUP_INFO
    {
        public byte MaximumProcessorCount;
        public byte ActiveProcessorCount;
        private UInt64 Reserved_00_07;
        private UInt64 Reserved_08_15;
        private UInt64 Reserved_16_23;
        private UInt64 Reserved_24_31;
        private UInt32 Reserved_32_35;
        private UInt16 Reserved_36_37;
        public UIntPtr Affinity;
    }
}
