using my_website.Controllers.Console.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_website.Controllers.Console.Commands.Interfaces
{
    interface IConsoleCommandStrategy<T>
    {
        T Execute(ConsoleCommandVariableAttribute.Vo[] objs);
    }
}
