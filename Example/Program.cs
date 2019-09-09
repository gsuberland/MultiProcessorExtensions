using System;
using System.Diagnostics;
using MultiProcessorExtensions;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // print information about group affinities for processor packages
            var packages = MultiProcessorInformation.GetProcessorPackageInfo();
            for (int i = 0; i < packages.Length; i++)
            {
                Console.WriteLine($"Processor Package {i}:");
                foreach (var affinity in packages[i].Affinities)
                {
                    Console.WriteLine($"\tAffinity: {affinity.Group} (mask: {affinity.Mask.ToUInt64():x16})");
                }
            }
            Console.WriteLine();

            // print information about group affinities for processor cores
            var cores = MultiProcessorInformation.GetProcessorCoreInfo();
            Console.WriteLine($"Core Count: {cores.Length}");
            for (int i = 0; i < cores.Length; i++)
            {
                Console.WriteLine($"Processor Core {i} (SMT: {cores[i].IsSMT}, Efficiency Class {cores[i].EfficiencyClass})");
                foreach (var affinity in cores[i].Affinities)
                {
                    Console.WriteLine($"\tAffinity: {affinity.Group} (Mask: {affinity.Mask.ToUInt64():x16})");
                }
            }
            Console.WriteLine();

            // print information about processor caches and their associated affinities
            var caches = MultiProcessorInformation.GetCacheInfo();
            Console.WriteLine($"Cache Count: {caches.Length}");
            for (int i = 0; i < caches.Length; i++)
            {
                Console.WriteLine($"Cache {i}:");
                Console.WriteLine($"\t Level:          {caches[i].Level}");
                Console.WriteLine($"\t Associativity:  {caches[i].Associativity}");
                Console.WriteLine($"\t Line Size:      {caches[i].LineSize}");
                Console.WriteLine($"\t Cache Size:     {caches[i].Level}");
                Console.WriteLine($"\t Group Number:   {caches[i].Affinity.Group}");
                Console.WriteLine($"\t Group Mask:     {caches[i].Affinity.Mask.ToUInt64():x16}");
            }
            Console.WriteLine();

            // print information about processor groups on the system
            var groupInfo = MultiProcessorInformation.GetGroupsInfo();
            Console.WriteLine($"Maximum Group Count: {groupInfo.MaximumGroupCount}");
            Console.WriteLine($"Active Group Count:  {groupInfo.ActiveGroupCount}");
            Console.WriteLine("Active Groups:");
            for (int i = 0; i < groupInfo.Groups.Length; i++)
            {
                Console.WriteLine($"\tGroup {i} (Active Count: {groupInfo.Groups[i].ActiveProcessorCount}, Maximum Count: {groupInfo.Groups[i].MaximumProcessorCount}, Mask: {groupInfo.Groups[i].ActiveProcessorMask.ToUInt64():x16})");
            }
            Console.WriteLine();

            // get the process group numbers associated with the current process
            ushort[] processGroups = Process.GetCurrentProcess().GetGroupAffinity();
            Console.WriteLine("Current Process Group Numbers: {" + string.Join(", ", processGroups) + "}");
            Console.WriteLine();

            // print the group affinities for all native threads in the current process
            foreach (ProcessThread pt in Process.GetCurrentProcess().Threads)
            {
                var affinity = pt.GetProcessorGroup();
                Console.WriteLine($"Thread {pt.Id.ToString().PadRight(8, ' ')} is assigned group {affinity.Group.ToString().PadRight(2, ' ')} (mask {affinity.Mask.ToUInt64():x16})");
            }
            Console.WriteLine();

            // set the processor group for a native thread to group number 0 with a mask of 0xff (the default)
            Process.GetCurrentProcess().Threads[0].SetProcessorGroup(0, new UIntPtr(0xff));
        }
    }
}
