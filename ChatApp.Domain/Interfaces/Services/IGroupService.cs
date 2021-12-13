using ChatApp.Domain.Enums;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IGroupService
    {
        IResponse GetGroupById(int groupId);
        IResponse Register(string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup);
        IResponse RemoveGroup(int groupId);
    }
}
