﻿using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IAppService
    {
        //--------------
        IResponse AccountUpdateUser(int id, string Username, string Emailaddress, string Password);
        IResponse BlockUser(int userId);
        IResponse ListUsers(User user, GroupTypeEnum groupType);
        IResponse LoginUser(string Username, string Password);
        IResponse RegisterUser(string Name, string Username, string Emailaddress, string Password);
        //--------------
        IResponse InviteGroup(User user, int InviteId);
        IResponse JoinGroup(User sender, int GroupId, int UserId, string Message);
        IResponse ListGroups(int GroupId, User user);
        IResponse RegisterGroup(int UserId, string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup);
        IResponse RegisterGroup(User user, string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup);
        IResponse RemoveGroup(int GroupId);
        IResponse RemoveUserFromGroup(int userId, int GroupId);
        //--------------
    }
}
