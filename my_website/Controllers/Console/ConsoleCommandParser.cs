using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    public class ConsoleCommandParser
    {
        public static string Parse(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
                return null;

            string[] ss = cmd.Split(" "[0]);

            if(ss.Length >= 2)
            {
                switch (ss[0])
                {
                    case "goto":
                        return "So, you want to go to " + ss[1];
                }
            }

            return null;
        }
    }
}