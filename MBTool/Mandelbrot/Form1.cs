using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using my_website.Controllers.MandelbrotSet.MovieMaker;
using System.Threading;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        private Mandelbrot mb = new Mandelbrot();
        private Dictionary<int, Bitmap> framesDic;

        public Form1()
        {
            mb = new Mandelbrot();
            InitializeComponent();

            textBox1.Text = "-0.53";
            textBox2.Text = "-0.53";
            textBox3.Text = "-0.52";
            textBox4.Text = "-0.52";

            textBox8.Text = "-0.523";
            textBox7.Text = "-0.523";
            textBox6.Text = "-0.522";
            textBox5.Text = "-0.522";
            textBox9.Text = "100";

            textBox11.Text = "250";
            textBox12.Text = "3";

            textBox13.Text = "41";
            textBox14.Text = "230";

            textBox15.Text = "4";
            textBox16.Text = "0.9";

            textBox17.Text = "700";
            textBox18.Text = "700";
        }

        private int GetLinearSum(int n)
        {
            int result = 0;
            for (int i = 1; i <= n; i++)
            {
                result += i;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            framesDic = new Dictionary<int, Bitmap>();

            double minX = double.Parse(textBox1.Text);
            double minY = double.Parse(textBox2.Text);

            double maxX = double.Parse(textBox3.Text);
            double maxY = double.Parse(textBox4.Text);

            double tomaxY = double.Parse(textBox5.Text);
            double tomaxX = double.Parse(textBox6.Text);

            double tominY = double.Parse(textBox7.Text);
            double tominX = double.Parse(textBox8.Text);

            int frames = int.Parse(textBox9.Text);

            double linearSum = GetLinearSum(frames);

            double xMinParticle = ((double)(tominX - minX) / linearSum);
            double xMaxParticle = ((double)(tomaxX - maxX) / linearSum);
            double yMinParticle = ((double)(tominY - minY) / linearSum);
            double yMaxParticle = ((double)(tomaxY - maxY) / linearSum);

            //textBox10.Text = "toAddminX: " + toAddminX + "toAddminY: " + toAddminY + "toAddmaxX: " + toAddmaxX + "toAddmaxY: " + toAddmaxY;

            int linearSumOFrames = GetLinearSum(frames);

            int k = int.Parse(textBox11.Text);
            double power = double.Parse(textBox12.Text);
            double power2 = double.Parse(textBox15.Text);
            float light = float.Parse(textBox16.Text);

            int startHue = int.Parse(textBox13.Text);
            int endHue = int.Parse(textBox14.Text);

            int width = int.Parse(textBox17.Text);
            int height = int.Parse(textBox18.Text);

            int cpusCount = Environment.ProcessorCount;
            var threads = new List<Thread>();

            for (int i = 0; i < frames; i++)
            {
                int key = i;
                Thread thread = new Thread(new ThreadStart(delegate { framesDic.Add(key, mb.GetImage(width, height, minX, maxY, minY, maxY, k, power, startHue, endHue, power2, light)); }));
                threads.Add(thread);
                thread.Start();
                cpusCount--;

                if (cpusCount == 0 || i == frames - 1)
                {
                    foreach (var thrd in threads)
                        thrd.Join();
                    cpusCount = Environment.ProcessorCount;
                }

                double toAddminX = xMinParticle * (double)(frames - i);
                double toAddmaxX = xMaxParticle * (double)(frames - i);
                double toAddminY = yMinParticle * (double)(frames - i);
                double toAddmaxY = yMaxParticle * (double)(frames - i);

                minX += toAddminX;
                minY += toAddminY;
                maxX += toAddmaxX;
                maxY += toAddmaxY;
            }

            var orderedFrames = framesDic.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            /*
            using (MemoryStream msGif = new MemoryStream())
            {
                GifWriter gifWriter = new GifWriter(msGif, 50, -1);

                foreach (var bitmap in orderedFrames)
                {
                    using (MemoryStream msImage = new MemoryStream())
                    {
                        bitmap.Save(msImage, System.Drawing.Imaging.ImageFormat.Png);
                        gifWriter.WriteFrame(Image.FromStream(msImage));
                    }

                }

                File.WriteAllBytes("test.gif", msGif.GetBuffer());
            }
            */

            foreach (var kvp in orderedFrames)
            {
                using (FileStream msImage = new FileStream(kvp.Key + ".png", FileMode.Create))
                {
                    kvp.Value.Save(msImage, System.Drawing.Imaging.ImageFormat.Png);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double minX = double.Parse(textBox1.Text);
            double minY = double.Parse(textBox2.Text);

            double maxX = double.Parse(textBox3.Text);
            double maxY = double.Parse(textBox4.Text);

            int k = int.Parse(textBox11.Text);
            double power = double.Parse(textBox12.Text);
            double power2 = double.Parse(textBox15.Text);
            float light = float.Parse(textBox16.Text);

            int startHue = int.Parse(textBox13.Text);
            int endHue = int.Parse(textBox14.Text);

            int width = int.Parse(textBox17.Text);
            int height = int.Parse(textBox18.Text);

            MemoryStream ms = new MemoryStream();
            mb.GetImage(width, height, minX, maxY, minY, maxY, k, power, startHue, endHue, power2, light).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.Image = Image.FromStream(ms);
        }
    }
}
