using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses;
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

        //public IResponse GetGroupById(int groupId)
        //{
        //    IResponse response = new Response(MethodCode.GetGroupById, LayerCode.Service, groupId);

        //    return response.Successfull(_GroupRepository.GetGroupByID(groupId));
        //}

        //public IResponse Create(string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        //{
        //    IResponse response = new Response(MethodCode.GetGroupById, LayerCode.Service,
        //        new object[] { Name, Password, MaxUsers, Visibility, GroupType });

        //    Group group = new Group()
        //    {
        //        Name = Name,
        //        MaxUsers = MaxUsers,
        //        VisibilityType = Visibility,
        //        Password = Password,
        //        type = GroupType
        //    };

        //    _GroupRepository.InsertGroup(group);
        //    _GroupRepository.Save();

        //    return response.Successfull(group);
        //}

        //public IResponse RemoveGroup(int groupId)
        //{
        //    IResponse response = new Response(MethodCode.GetGroupById, LayerCode.Service,
        //        groupId);
        //    _GroupRepository.DeleteGroup(groupId);
        //    return response.Successfull();
        //}
    }
}
