﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapApp.Business.Domain.Interfaces
{
    public interface IRepository
    {
        object _dataContext { get; }
    }
}