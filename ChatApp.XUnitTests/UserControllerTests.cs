using ChatApp.Business.Core.AppServices;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChatApp.XUnitTests
{
    public class UserControllerTests
    {
        User user;
        GroupTypeEnum groupType;

        IAppService _appService;


        [Fact]
        public void GetUserListByGroupType()
        {
            // Create the mock
            var mock = new Mock<IAppService>();

            // Configure the mock to do something
            mock.SetupGet(x => x.ListUsers).Returns(this.CreateResponse());

            // Demonstrate that the configuration works
            Assert.AreEqual("FixedValue", mock.Object.PropertyToMock);

            // Verify that the mock was invoked
            mock.VerifyGet(x => x.PropertyToMock);

        }
    }
}
