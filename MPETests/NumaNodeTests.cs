using System;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class NumaNodeTests
    {
        [Fact]
        public void NumaNodeInfoReturnsData()
        {
            var nodes = MultiProcessorInformation.GetNumaNodeInfo();
            Assert.NotEmpty(nodes);
        }

        [Fact]
        public void NumaNodeAffinitiesAreNotZero()
        {
            var nodes = MultiProcessorInformation.GetNumaNodeInfo();
            foreach (var node in nodes)
            {
                Assert.True(node.Affinity.Mask != UIntPtr.Zero);
            }
        }
    }
}
