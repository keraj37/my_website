using my_website.Models;
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
    [ConsoleCommandVariable(KeyName = "width", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 700)]
    [ConsoleCommandVariable(KeyName = "height", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 700)]
    [ConsoleCommandVariable(KeyName = "k", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 250, MaxIntValue = 1500)]
    [ConsoleCommandVariable(KeyName = "step", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 1)]
    [ConsoleCommandVariable(KeyName = "ymin", Type = ConsoleCommandVariableAttribute.CommandValueType.DOUBLE, DefaultValue = -2d)]
    [ConsoleCommandVariable(KeyName = "ymax", Type = ConsoleCommandVariableAttribute.CommandValueType.DOUBLE, DefaultValue = 2d)]
    [ConsoleCommandVariable(KeyName = "xmin", Type = ConsoleCommandVariableAttribute.CommandValueType.DOUBLE, DefaultValue = -2d)]
    [ConsoleCommandVariable(KeyName = "xmax", Type = ConsoleCommandVariableAttribute.CommandValueType.DOUBLE, DefaultValue = 2d)]
    [ConsoleCommandVariable(KeyName = "power", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, DefaultValue = 4f)]
    [ConsoleCommandVariable(KeyName = "power2", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, DefaultValue = 3f)]
    [ConsoleCommandVariable(KeyName = "light", Type = ConsoleCommandVariableAttribute.CommandValueType.FLOAT, DefaultValue = 0.9f)]
    [ConsoleCommandVariable(KeyName = "starthue", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 41)]
    [ConsoleCommandVariable(KeyName = "endhue", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 230)]
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
            double lastWidth = vals.GetValue("xmax").DoubleValue - vals.GetValue("xmin").DoubleValue;
            ConsoleMandelbrotReturnVo result = new ConsoleMandelbrotReturnVo("\nMandelbrotSet: applied values...", image: img.ImageBase64, lastX: vals.GetValue("xmin").DoubleValue, lastY: vals.GetValue("ymin").DoubleValue, lastWidth: lastWidth);
            return result;
        }
    }
}