using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
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

        }
    }
}
