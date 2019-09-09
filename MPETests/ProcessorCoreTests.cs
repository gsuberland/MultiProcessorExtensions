using System;
using System.Linq;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class ProcessorCoreTests
    {
        [Fact]
        public void ProcessorCoreInfoReturnsData()
        {
            var cores = MultiProcessorInformation.GetProcessorCoreInfo();
            Assert.NotEmpty(cores);
        }

        [Fact]
        public void ProcessorCoreGroupMaskIsNotEmpty()
        {
            var cores = MultiProcessorInformation.GetProcessorCoreInfo();
            foreach (var core in cores)
            {
                Assert.NotEmpty(core.Affinities);
            }
        }

        [Fact]
        public void ProcessorCoreGroupMaskIsNotZero()
        {
            var cores = MultiProcessorInformation.GetProcessorCoreInfo();
            foreach (var core in cores)
            {
                foreach (var affinity in core.Affinities)
                {
                    Assert.True(affinity.Mask != UIntPtr.Zero);
                }
            }
        }

        [Fact]
        public void ProcessorCoreInfoMatchesProcessorCount()
        {
            var cores = MultiProcessorInformation.GetProcessorCoreInfo();
            int coreCount = cores.Sum(core => core.IsSMT ? 2 : 1);
            Assert.Equal(coreCount, Environment.ProcessorCount);
        }
    }
}
