using ChatApp.Domain.Enums.ResponseCodes;
using System.Collections.Generic;

namespace ChatApp.Business.Domain.Responses
{
    public class ResponseCode //2-1-14
    {
        public LayerCode LayerCode { get; init; } //x
        public MethodCode MethodCode { get; init; } //x-x

        public List<int> ToCodes()
        {
            return new List<int>() { (int)LayerCode, (int)MethodCode };
        }

        public static ResponseCode NewCode(
            LayerCode layerCode,
            MethodCode methodCode)
        {
            return new ResponseCode()
            {
                LayerCode = layerCode,
                MethodCode = methodCode
            };
        }
    }
}
