using ChatApp.Business.Domain.Responses;
using System.Collections.Generic;

namespace ChatApp.Domain.Interfaces
{
    public interface IResponse
    {
        bool Valid { get; init; }
        ResponseCode Code { get; set; }
        List<int> ToCodes();
    }
}
