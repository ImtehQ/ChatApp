using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
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

        public IResponse Create(string name, string password, int maxUsers = 0, GroupVisibilityEnum visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum groupType = GroupTypeEnum.OptionGroup)
        {
            throw new System.NotImplementedException();
        }

        public IResponse GetGroupById(int groupId)
        {
            IResponse response = this.CreateResponse();
            var group = _GroupRepository.GetGroupByID(groupId);
            response.Contents(group);
            return response.Successfull();
        }

        public IResponse Register(string name, string password, int maxUsers = 0, GroupVisibilityEnum visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum groupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();
            Group group = new Group()
            {
                Name = name,
                MaxUsers = maxUsers,
                VisibilityType = visibility,
                Password = password,
                type = groupType
            };
            response.Contents(group);
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
