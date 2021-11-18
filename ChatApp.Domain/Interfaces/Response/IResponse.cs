using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IResponse
    {
        public bool IsValid { get; init; }
        public bool IsNotValid { get; }
        public string Message { get; init; }
    }
}
