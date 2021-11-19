using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IGroupService
    {
        Group Create(string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup);
    }
}
