using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChatApp.XUnitTests.GroupAppServiceTests
{
    public class List : TestBase
    {
        [Fact]
        public void ListGroups()
        {
            IResponse response = appService.ListGroups(0, 2);

            Assert.False(response.GetValid());
        }
    }
}
