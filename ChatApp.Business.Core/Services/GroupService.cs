using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.Services
{

    public class GroupService : IGroupService
    {
        IGenericRepository<Group> _GroupRepository;

        public GroupService(IGenericRepository<Group> groupRepository)
        {
            _GroupRepository = groupRepository;
        }

        public IResponse GetGroupById(int groupId)
        {
            IResponse response = this.CreateResponse();
            var group = _GroupRepository.GetById(groupId);
            response.SetAttachment(group);
            return response.Successfull();
        }

        public IResponse Register(string name, string password, int maxUsers = 0, GroupVisibilityEnum visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum groupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(name))
                return response.Failed("name is invalid");
            if (maxUsers < 2)
                return response.Failed("maxUsers can't be below 2");

            Group group = new Group()
            {
                Name = name,
                MaxUsers = maxUsers,
                VisibilityType = visibility,
                Password = password,
                type = groupType
            };
            response.SetAttachment(group);
            _GroupRepository.Insert(group);
            _GroupRepository.Save();

            return response.Successfull();
        }

        public IResponse RemoveGroup(int groupId)
        {
            IResponse response = this.CreateResponse();
            _GroupRepository.Delete(groupId);
            return response.Successfull();
        }
    }
}
