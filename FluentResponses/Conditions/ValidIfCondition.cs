using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.Conditions
{
    public class ValidIfCondition
    {
        IResponse Response { get; set; }
        public ValidIfCondition(IResponse response)
        {
            Response = response;
        }

        public IResponse ContentIsNotNull()
        {
            if (Response.GetContent() != null)
                Response.IsValid = true;
            return Response;
        }
        public IResponse ContentIsNull()
        {
            if (Response.GetContent() == null)
                Response.IsValid = true;
            return Response;
        }
    }
}
}
