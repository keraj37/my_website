using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    public abstract class BaseCommand
    {
        public virtual string Execute(string[] cmd)
        {
            return null;
        }
    }
}