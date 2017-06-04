using my_website.Controllers.MandelbrotSet.Solution01;
using my_website.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace my_website.Controllers.MandelbrotSet
{
    public class MandelbrotSetManager
    {
        public Mandelbrot alghorithm = new Mandelbrot();

        public FractalImage GetImage(int k = 50, int zoom = 1, int step = 1)
        {
            Bitmap bmp = new Bitmap(900, 600);
            alghorithm.RenderImageToBitmap(bmp, kMaxParam: k, zoomScaleParam: zoom, xyPixelStepParam: step);

            byte[] bytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bmp.Save(memoryStream, ImageFormat.Png);
                bytes = memoryStream.GetBuffer();
            }

            FractalImage img = new FractalImage();
            img.Name = "Mandelbrot Set";
            img.ImageBase64 = Convert.ToBase64String(bytes);

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