using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IRepository<T>
    {
        T Context { get; set; }
    }
}
