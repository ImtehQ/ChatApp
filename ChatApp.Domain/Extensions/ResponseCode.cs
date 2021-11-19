﻿using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Domain.Responses
{
    public class ResponseCode //2-1-14
    {
        public ResponseLayerCode LayerCode { get; init; } //x
        public ResponseMethodCode MethodCode { get; init; } //x-x
        public ResponseMessageCode MessageCode { get; init; } //x-x-x

        public List<int> ToCodes()
        {
            return new List<int>() { (int)LayerCode, (int)MethodCode, (int)MessageCode };
        }

        public static ResponseCode NewCode(
            ResponseLayerCode layerCode, 
            ResponseMethodCode methodCode, 
            ResponseMessageCode mssageCode)
        {
            return new ResponseCode()
            {
                LayerCode = layerCode,
                MethodCode = methodCode,
                MessageCode = mssageCode
            };
        }
    }
}