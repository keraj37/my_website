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
            if (!string.IsNullOrEmpty(message))
            {
                bool addNewLine = message[0] != '\n' ? true : false;
                Message = (addNewLine ? "\n" : string.Empty) + message;
            }
            
            ToAction = toAction;
            FillInput = fillInput;
        }

        public virtual object ToObject()
        {
            return new { content = Message, redirectToAction = ToAction, fillInput = FillInput };
        }
    }
}