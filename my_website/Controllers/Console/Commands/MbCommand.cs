﻿using my_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.MandelbrotSet;
using my_website.Controllers.Console.Commands.Interfaces;
using my_website.Controllers.Console.Commands.Attributes;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommandDescription(Name = "mb ('M'andel'b'rot)", Description = "Mandelbrot Set modification command\n\tUse it with console input from 'MandelbrotSet' site", Priority = 4)]
    [ConsoleCommandVariable(KeyName = "width", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 0)]
    [ConsoleCommandVariable(KeyName = "height", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 1)]
    [ConsoleCommandVariable(KeyName = "zoom", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 2)]
    [ConsoleCommandVariable(KeyName = "k", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 3)]
    [ConsoleCommandVariable(KeyName = "step", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 4)]
    public class MbCommand : BaseCommand
    {
        public MbCommand(Controller controller = null) : base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleCommandVariableAttribute.Vo[] objs = GetAllValuesInOrder(cmd);
            IConsoleCommandStrategy<FractalImage> mbset = new MandelbrotSetManager();
            FractalImage img = mbset.Execute(objs);
            ConsoleMandelbrotReturnVo result = new ConsoleMandelbrotReturnVo("\nMandelbrotSet: applied values...", image: img.ImageBase64);
            return result;
        }
    }
}