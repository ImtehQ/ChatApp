﻿using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
        public AccountRoleEnum AccountRole { get; set; }
    }
}
