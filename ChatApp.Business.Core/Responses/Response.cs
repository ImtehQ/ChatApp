using ChatApp.Business.Domain.Responses;
using ChatApp.Domain.Interfaces;
using System.Collections.Generic;

namespace ChatApp.Business.Core.Responses
{
    public class Response : IResponse
    {
        public bool Valid { get; init; }
        public ResponseCode Code { get; set; }
        public List<Response> Responses { get; set; }
        public object Content { get; init; }

        public List<int> ToCodes()
        {
            List<int> codes = new List<int>();
            codes.AddRange(Code.ToCodes());
            for (int i = 0; i < Responses.Count; i++)
            {
                codes.AddRange(Responses[i].ToCodes());
            }
            return codes; 
        }

        public static Response Successfull()
        {
            return new Response() { Valid = true, Responses = null, Content = null, Code = null };
        }
    }
}
