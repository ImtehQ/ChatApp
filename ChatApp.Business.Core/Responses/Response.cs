using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Responses
{
    public abstract class Response : IResponse
    {
        public bool IsValid { get; init; }
        public bool IsNotValid { get { return !IsValid; } }
        public string Message { get; init; }
    }
}
