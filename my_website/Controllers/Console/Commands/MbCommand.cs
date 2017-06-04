using my_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.MandelbrotSet;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommandDescription(Name = "mb ('M'andel'b'rot)", Description = "Mandelbrot Set modification command\n\tUse it with console input from 'MandelbrotSet' site", Priority = 4)]
    public class MbCommand : BaseCommand
    {
        public MbCommand(Controller controller = null) : base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            Dictionary<string, string> dic = GetAllKeyValues(cmd);

            int zoom = 1;
            int k = 50;
            int step = 1;
            string msg = string.Empty;

            foreach (var kvp in dic)
            {
                switch (kvp.Key)
                {
                    case "zoom":
                        if (int.TryParse(kvp.Value, out zoom))
                            msg += "\nMandelbrot: Setting zoom to " + kvp.Value;
                        else
                            msg += "\nMandelbrot: wrong zoom value: " + kvp.Value;
                        break;
                    case "k":
                        if (int.TryParse(kvp.Value, out k))
                            msg += "\nMandelbrot: Setting k to " + kvp.Value;
                        else
                            msg += "\nMandelbrot: wrong k value: " + kvp.Value;
                        break;
                    case "step":
                        if (int.TryParse(kvp.Value, out step))
                            msg += "\nMandelbrot: Setting step to " + kvp.Value;
                        else
                            msg += "\nMandelbrot: wrong step value: " + kvp.Value;
                        break;
                }
            }

            MandelbrotSetManager mbset = new MandelbrotSetManager();
            FractalImage img = mbset.GetImage(k, zoom, step);
            ConsoleMandelbrotReturnVo result = new ConsoleMandelbrotReturnVo(msg, image: img.ImageBase64);
            return result;
        }
    }
}