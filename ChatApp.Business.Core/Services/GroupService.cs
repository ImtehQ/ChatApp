using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.Services
{

    public class GroupService : IGroupService
    {
        IGroupRepository _GroupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _GroupRepository = groupRepository;
        }

        public IResponse GetGroupById(int groupId)
        {
            IResponse response = this.CreateResponse();
            var group = _GroupRepository.GetGroupByID(groupId);
            response.Contents(group);
            return response.Successfull();
        }

        public IResponse Create(string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();
            Group group = new Group()
            {
                Name = Name,
                MaxUsers = MaxUsers,
                VisibilityType = Visibility,
                Password = Password,
                type = GroupType
            };

            _GroupRepository.InsertGroup(group);
            _GroupRepository.Save();

            return response.Successfull();
        }

        public IResponse RemoveGroup(int groupId)
        {
            IResponse response = this.CreateResponse();
            _GroupRepository.DeleteGroup(groupId);
            return response.Successfull();
        }
    }
}
