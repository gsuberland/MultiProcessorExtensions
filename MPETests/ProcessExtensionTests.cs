using System;
using System.Linq;
using System.Diagnostics;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class ProcessorExtensionTests
    {
        [Fact]
        public void CanGetProcessorGroup()
        {
            var proc = Process.GetCurrentProcess();
            ushort[] groups = proc.GetGroupAffinity();
            Assert.NotNull(groups);
            Assert.NotEmpty(groups);
        }
    }
}
