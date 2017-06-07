﻿using my_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.MandelbrotSet;
using my_website.Controllers.Console.Commands.Interfaces;
using my_website.Controllers.Console.Commands.Attributes;
using System.Reflection;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommandDescription(Name = "mb ('M'andel'b'rot)", Description = "Mandelbrot Set modification command\n\tUse it with console input from 'MandelbrotSet' site", Priority = 4)]
    [ConsoleCommandVariable(KeyName = "width", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 0, DefaultValue = 700)]
    [ConsoleCommandVariable(KeyName = "height", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 1, DefaultValue = 700)]
    [ConsoleCommandVariable(KeyName = "zoom", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 2, DefaultValue = 7)]
    [ConsoleCommandVariable(KeyName = "k", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 3, DefaultValue = 50)]
    [ConsoleCommandVariable(KeyName = "step", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 4, DefaultValue = 1)]
    [ConsoleCommandVariable(KeyName = "ymin", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 5, DefaultValue = -2f)]
    [ConsoleCommandVariable(KeyName = "ymax", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 6, DefaultValue = 2f)]
    [ConsoleCommandVariable(KeyName = "xmin", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 7, DefaultValue = -2f)]
    [ConsoleCommandVariable(KeyName = "xmax", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 8, DefaultValue = 2f)]
    [ConsoleCommandVariable(KeyName = "colpow", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 9, DefaultValue = 0.15f)]
    [ConsoleCommandVariable(KeyName = "colhue", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 10, DefaultValue = 0.8f)]
    [ConsoleCommandVariable(KeyName = "collight", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 11, DefaultValue = 0.52f)]
    [ConsoleCommandVariable(KeyName = "colshift", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 12, DefaultValue = 0)]
    [ConsoleCommandVariable(KeyName = "colshift2", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, Order = 13, DefaultValue = 0)]
    [ConsoleCommandVariable(KeyName = "colneg", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 14, DefaultValue = 0f)]
    [ConsoleCommandVariable(KeyName = "colshiftconst", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, Order = 15, DefaultValue = 0f)]
    [ConsoleCommandStrategy(typeof(MandelbrotSetManager))]
    public class MbCommand : BaseCommand
    {
        public MbCommand(Controller controller = null) : base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleCommandVariableAttribute.Values vals = GetAllValuesInOrder(cmd);
            IConsoleCommandStrategy mbset = this.GetType().GetCustomAttribute<ConsoleCommandStrategyAttribute>().GetInstance();
            FractalImage img = (FractalImage)mbset.Execute(vals);
            ConsoleMandelbrotReturnVo result = new ConsoleMandelbrotReturnVo("\nMandelbrotSet: applied values...", image: img.ImageBase64);
            return result;
        }
    }
}