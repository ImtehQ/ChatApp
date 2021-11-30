using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Business.Core.AppServices
{
    //Group
    public partial class AppService : IAppService
    {
        //public IResponse List(int GroupId, int UserId)
        //{
        //    IResponse response = new Response(MethodCode.List, LayerCode.Service, GroupId);

        //    IResponse UserResponse = _UserService.GetUserById(UserId);

        //    return response.Link(_GroupUserService.GetGroupsByUser(UserResponse.GetResponseObject<User>()));
        //}

        //public IResponse Register(int UserId, string Name, string Password, int MaxUsers = 0,
        //    GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic,
        //    GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        //{
        //    IResponse response = new Response(MethodCode.Register, LayerCode.Service, Name);

        //    IResponse UserResponse = _UserService.GetUserById(UserId);
        //    response.Link(UserResponse);
        //    if (UserResponse.Valid == false) return response;

        //    IResponse GroupResponse = _GroupService.Create(Name, Password, MaxUsers, Visibility, GroupType);
        //    response.Link(GroupResponse);
        //    if (GroupResponse.Valid == false) return response;

        //    return response.Link(_GroupUserService.Insert(
        //        UserResponse.GetResponseObject<User>(),
        //        GroupResponse.GetResponseObject<Group>(), 
        //        AccountRoleEnum.RoleAdmin));
        //}
   

        //public IResponse Invite(int UserId, int InviteId)
        //{
        //    IResponse response = this.CreateResponse();



            

        //    IResponse res = new Bfet(response, MethodCode.Invite, LayerCode.Service, null);


        //    IResponse UserResponse = _UserService.GetUserById(UserId);
        //    response.Link(UserResponse);



        //    if (UserResponse.Valid == false) return response;

        //    IResponse InviteResponse = _InviteService.GetInviteById(InviteId);
        //    response.Link(InviteResponse);
        //    if (InviteResponse.Valid == false) return response;

        //    IResponse GroupResponse = _GroupService.GetGroupById(InviteResponse.GetResponseObject<Invite>().GroupId);
        //    response.Link(GroupResponse);
        //    if (GroupResponse.Valid == false) return response;

        //    IResponse GroupUserResponse = _GroupUserService.Insert(
        //        UserResponse.GetResponseObject<User>(),
        //        GroupResponse.GetResponseObject<Group>(),
        //        AccountRoleEnum.RoleUser);
        //    response.Link(GroupResponse);
        //    if (GroupResponse.Valid == false) return response;

        //    return response;
        //}

        //public IActionResult Join(int GroupId, int UserId, string Message)
        //{
        //    return appService.Join(int GroupId, int UserId, string Message);

        //    User User = _UserService.GetUserById(UserId).GetResponseObject<User>();

        //    User senderUser = _UserService.GetUserById(HttpContext.User.GetUserID()).GetResponseObject<User>();

        //    Group group = _GroupService.Create("Invite chat", "", 2,
        //        GroupVisibilityEnum.OptionPrivate, GroupTypeEnum.OptionPrivate).GetResponseObject<Group>();

        //    _GroupUserService.Join(group, senderUser, AccountRoleEnum.RoleAdmin);

        //    _GroupUserService.Join(group, User, AccountRoleEnum.RoleUser);

        //    Invite invite = new Invite() { GroupId = group.GroupId, Message = Message };

        //    _InviteService.Register(invite);

        //    _MessageService.SendMessage(Message, senderUser, GroupTypeEnum.OptionPrivate, group.GroupId);
        //    _MessageService.SendMessage($"Invite: {invite.Id}", senderUser, GroupTypeEnum.OptionPrivate, group.GroupId);

        //    return Ok("Not implimantata");
        //}

        //public IResponse RemoveGroup(int GroupId)
        //{
        //    IResponse response = new Response(MethodCode.Invite, LayerCode.Service, GroupId);

        //    IResponse GroupResponse = _GroupService.GetGroupById(GroupId);
        //    response.Link(GroupResponse);
        //    if (GroupResponse.Valid == false) return response;

        //    IResponse GroupUserResponse = _GroupUserService.RemoveGroup(GroupResponse.GetResponseObject<Group>());
        //    response.Link(GroupUserResponse);
        //    if (GroupUserResponse.Valid == false) return response;

        //    IResponse GroupRemoveResponse = _GroupService.RemoveGroup(GroupId);
        //    response.Link(GroupRemoveResponse);
        //    if (GroupRemoveResponse.Valid == false) return response;

        //    return response;
        //}

        //public IActionResult RemoveUser(int userId, int GroupId)
        //{
        //    Group group = _GroupService.GetGroupById(GroupId).GetResponseObject<Group>();
        //    if (group == null)
        //        return NotFound("Group");

        //    User user = _UserService.GetUserById(userId).GetResponseObject<User>();
        //    if (user == null)
        //        return NotFound("User");

        //    if ((int)_GroupUserService.GetAccountRoleByUser(user, group)
        //        .GetResponseObject<AccountRoleEnum>() < (int)AccountRoleEnum.RoleModerator)
        //    {
        //        return StatusCode(404);
        //    }

        //    _GroupUserService.RemoveUser(user, group);

        //    return Ok();
        //}
    }
}
