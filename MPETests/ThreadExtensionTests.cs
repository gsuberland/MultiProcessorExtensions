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
    }
}
