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
            var groupInfo = MultiProcessorInformation.GetGroupsInfo();
            Assert.NotEmpty(groupInfo.Groups);
        }

        [Fact]
        public void GroupMasksAreNotZero()
        {
            var groupInfo = MultiProcessorInformation.GetGroupsInfo();
            foreach (var group in groupInfo.Groups)
            {
                Assert.True(group.ActiveProcessorMask != UIntPtr.Zero);
            }
        }
    }
}
