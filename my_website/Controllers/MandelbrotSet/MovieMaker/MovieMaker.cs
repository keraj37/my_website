using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace my_website.Controllers.MandelbrotSet.MovieMaker
{
    public class MovieMaker
    {
        public Bitmap ToBitmap(byte[] byteArrayIn)
        {
            var ms = new System.IO.MemoryStream(byteArrayIn);
            var returnImage = System.Drawing.Image.FromStream(ms);
            var bitmap = new System.Drawing.Bitmap(returnImage);

            return bitmap;
        }

        public Bitmap ReduceBitmap(Bitmap original, int reducedWidth, int reducedHeight)
        {
            var reduced = new Bitmap(reducedWidth, reducedHeight);
            using (var dc = Graphics.FromImage(reduced))
            {
                dc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                dc.DrawImage(original, new Rectangle(0, 0, reducedWidth, reducedHeight), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }

            return reduced;
        }

        public static void CreateMovie(Bitmap[] frames, int width, int height, int frameRate = 24)
        {
            using (var vFWriter = new VideoFileWriter())
            {
                vFWriter.Open("mb.avi", width, height, frameRate, VideoCodec.Raw);

                foreach (var bmp in frames)
                {
                    //var imageByteArray = imageEntity.Data;
                    //var bmp = ToBitmap(imageByteArray);
                    //var bmpReduced = ReduceBitmap(bmp, width, height);

                    vFWriter.WriteVideoFrame(bmp);
                }

                vFWriter.Close();
            }
        }
    }
}