using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GroupAppServiceTests
{
    public class Create : TestBase
    {
        [Fact]
        public void Group_Name_Already_Exists()
        {
            IResponse response = this.CreateResponse();
            response.Include(GroupService.Register("", ""));


        }
    }
}
