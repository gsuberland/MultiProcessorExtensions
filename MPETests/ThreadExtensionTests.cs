using System;
using System.Linq;
using System.Diagnostics;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class ThreadExtensionTests
    {
        [Fact]
        public void CanGetProcessorGroup()
        {
            foreach (ProcessThread pt in Process.GetCurrentProcess().Threads)
            {
                var group = pt.GetProcessorGroup();
                // check that the data is sane (mask contains at least one bit set)
                Assert.NotEqual(UIntPtr.Zero, group.Mask);
            }
        }

        [Fact]
        public void CanSetProcessorGroup()
        {
            // get the first group so we can extract its affinity
            var firstGroup = MultiProcessorInformation.GetGroupsInfo().Groups.First();

            foreach (ProcessThread pt in Process.GetCurrentProcess().Threads)
            {
                // set the thread affinity to the first group on the system (group 0)
                pt.SetProcessorGroup(0, firstGroup.ActiveProcessorMask);

                // only process the first thread
                break;
            }
        }
    }
}
