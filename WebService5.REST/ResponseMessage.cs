using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class ResponseMessage
    {
        public string Message;
        public int ResponseCode;

        public ResponseMessage(string message, int rcode)
        {
            Message = message;
            ResponseCode = rcode;
        }

    }
}