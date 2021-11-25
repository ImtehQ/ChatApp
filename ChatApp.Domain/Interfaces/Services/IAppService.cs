using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IAppService
    {
        IResponse AccountUpdate(int id, string Username, string Emailaddress, string Password);
        IResponse BlockUser(int userId);
        IResponse List(int userId, GroupTypeEnum groupType);
        IResponse Login(string Username, string Password);
        IResponse Register(string Name, string Username, string Emailaddress, string Password);
    }
}
