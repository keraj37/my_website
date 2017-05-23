using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    public class ConsoleReturnVo
    {
        public string Message { get; set; }
        public string ToAction { get; set; }

        public ConsoleReturnVo(string message, string toAction = null)
        {
            Message = message;
            ToAction = toAction;
        }
    }
}