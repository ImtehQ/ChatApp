using ChapApp.Business.Domain.Interfaces;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Services
{
    public class GroupService : IGroupService
    {
        IGroupRepository _GroupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _GroupRepository = groupRepository;
        }

        public Group GetGroupById(int groupId)
        {
            return _GroupRepository.GetGroupByID(groupId);
        }

        public Group Create(string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
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

            return group;
        }

        public void RemoveGroup(int groupId)
        {
            _GroupRepository.DeleteGroup(groupId);
        }
    }
}
