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

namespace ChatApp.Business.Core.AppServices
{
    //GroupUser
    public partial class AppService : IAppService
    {
        public IResponse RegisterGroupUser(User user, Group group, AccountRoleEnum role)
        {
            return this.CreateResponse().Failed();
        }
    }
}
