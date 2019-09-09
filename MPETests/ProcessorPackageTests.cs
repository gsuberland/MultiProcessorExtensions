using System;
using System.Linq;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class ProcessorPackageTests
    {
        [Fact]
        public void ProcessorPackageInfoReturnsData()
        {
            var packages = MultiProcessorInformation.GetProcessorPackageInfo();
            Assert.NotEmpty(packages);
        }

        [Fact]
        public void ProcessorPackageGroupMaskIsNotEmpty()
        {
            var packages = MultiProcessorInformation.GetProcessorPackageInfo();
            foreach (var package in packages)
            {
                Assert.NotEmpty(package.Affinities);
            }
        }

        [Fact]
        public void ProcessorPackageGroupMaskIsNotZero()
        {
            var packages = MultiProcessorInformation.GetProcessorPackageInfo();
            foreach (var package in packages)
            {
                foreach (var affinity in package.Affinities)
                {
                    Assert.True(affinity.Mask != UIntPtr.Zero);
                }
            }
        }
    }
}
