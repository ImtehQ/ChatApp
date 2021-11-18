﻿using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int Id);
        IResponse Login(string username, string password);
        IResponse Register(string name, string username, string emailaddress, string password);
    }
}
