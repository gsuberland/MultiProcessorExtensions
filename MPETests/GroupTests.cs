using System;
using System.Linq;
using Xunit;
using MultiProcessorExtensions;

namespace MPETests
{
    public class GroupTests
    {
        [Fact]
        public void GroupInfoReturnsData()
        {
            var groups = MultiProcessorInformation.GetGroupsInfo();
            Assert.NotEmpty(groups);
        }

        [Fact]
        public void ProcessorPackageGroupMaskIsNotEmpty()
        {
            var groups = MultiProcessorInformation.GetGroupsInfo();
            foreach (var group in groups)
            {
                Assert.NotEmpty(group.Groups);
            }
        }

        [Fact]
        public void ProcessorPackageGroupMaskIsNotZero()
        {
            var groups = MultiProcessorInformation.GetGroupsInfo();
            foreach (var group in groups)
            {
                foreach (var subgroup in group.Groups)
                {
                    Assert.True(subgroup.ActiveProcessorMask != UIntPtr.Zero);
                }
            }
        }
    }
}
