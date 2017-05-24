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
        public string FillInput { get; set; }

        public ConsoleReturnVo(string message = null, string toAction = null, string fillInput = null)
        {
            Message = message;
            ToAction = toAction;
            FillInput = fillInput;
        }
    }
}