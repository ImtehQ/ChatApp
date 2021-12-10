using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;

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
