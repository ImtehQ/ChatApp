﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
