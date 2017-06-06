using my_website.Controllers.MandelbrotSet.Solution01;
using my_website.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using my_website.Controllers.Console.Commands.Interfaces;
using my_website.Controllers.Console.Commands.Attributes;

namespace my_website.Controllers.MandelbrotSet
{
    public class MandelbrotSetManager : IConsoleCommandStrategy
    {
        public object Execute(ConsoleCommandVariableAttribute.Vo[] objs)
        { 
            string bmpBase64 = Convert.ToBase64String(new Mandelbrot().GetImage(objs));
            FractalImage img = new FractalImage();
            img.Name = "Mandelbrot Set";
            img.ImageBase64 = bmpBase64;
            return img;
        }

        private void DrawRandom(Bitmap bmp)
        {
            Random r = new Random();
            for(int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    short byteMax = Byte.MaxValue + 1;
                    Color col = Color.FromArgb(r.Next(byteMax), r.Next(byteMax), r.Next(byteMax), r.Next(byteMax));
                    bmp.SetPixel(x, y, col);
                }
            }
        }
    }
}