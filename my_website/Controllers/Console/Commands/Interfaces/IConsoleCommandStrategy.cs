using my_website.Controllers.Console.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_website.Controllers.Console.Commands.Interfaces
{
    public interface IConsoleCommandStrategy
    {
        object Execute(ConsoleCommandVariableAttribute.Vo[] objs);
    }
}
