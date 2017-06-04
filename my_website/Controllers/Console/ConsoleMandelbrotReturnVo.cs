using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    public class ConsoleMandelbrotReturnVo : ConsoleReturnVo
    {
        public string Image { get; set; }

        public ConsoleMandelbrotReturnVo(string message = null, string toAction = null, string fillInput = null, string image = null):base(message, toAction, fillInput)
        {
            Image = image;
        }

        public override object ToObject()
        {
            return new { content = Message, redirectToAction = ToAction, fillInput = FillInput, image = Image };
        }
    }
}