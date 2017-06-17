using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    public class ConsoleMandelbrotReturnVo : ConsoleReturnVo
    {
        public string Image { get; set; }
        private double lastX;
        private double lastY;
        private double lastWidth;

        public ConsoleMandelbrotReturnVo(string message = null, string toAction = null, string fillInput = null, string image = null, 
            double lastX = default(double), double lastY = default(double), double lastWidth = default(double)) 
            :base(message, toAction, fillInput)
        {
            this.lastX = lastX;
            this.lastY = lastY;
            this.lastWidth = lastWidth;

            Image = image;
        }

        public override object ToObject()
        {
            return new { content = Message, redirectToAction = ToAction, fillInput = FillInput, image = Image, lastX = lastX, lastY = lastY, lastWidth = lastWidth };
        }
    }
}