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

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        private Mandelbrot mb = new Mandelbrot();

        public Form1()
        {
            mb = new Mandelbrot();
            InitializeComponent();

            textBox1.Text = "-0.6";
            textBox2.Text = "-0.6";
            textBox3.Text = "-0.5";
            textBox4.Text = "-0.5";

            textBox8.Text = "-0.53";
            textBox7.Text = "-0.53";
            textBox6.Text = "-0.52";
            textBox5.Text = "-0.52";
            textBox9.Text = "100";

            textBox11.Text = "250";
            textBox12.Text = "3";

            textBox13.Text = "41";
            textBox14.Text = "230";
        }

        private int GetLinearSum(int n)
        {
            int result = 0;
            for(int i = 1; i <= n; i++)
            {
                result += i;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float minX = float.Parse(textBox1.Text);
            float minY = float.Parse(textBox2.Text);

            float maxX = float.Parse(textBox3.Text);
            float maxY = float.Parse(textBox4.Text);

            float tomaxY = float.Parse(textBox5.Text);
            float tomaxX = float.Parse(textBox6.Text);

            float tominY = float.Parse(textBox7.Text);
            float tominX = float.Parse(textBox8.Text);

            int frames = int.Parse(textBox9.Text);

            float linearSum = GetLinearSum(frames);

            float xMinParticle = ((float)(tominX - minX) / linearSum);
            float xMaxParticle = ((float)(tomaxX - maxX) / linearSum);
            float yMinParticle = ((float)(tominY - minY) / linearSum);
            float yMaxParticle = ((float)(tomaxY - maxY) / linearSum);

            //textBox10.Text = "toAddminX: " + toAddminX + "toAddminY: " + toAddminY + "toAddmaxX: " + toAddmaxX + "toAddmaxY: " + toAddmaxY;

            int linearSumOFrames = GetLinearSum(frames);

            int k = int.Parse(textBox11.Text);
            float power = float.Parse(textBox12.Text);

            int startHue = int.Parse(textBox13.Text);
            int endHue = int.Parse(textBox14.Text);

            using (MemoryStream msGif = new MemoryStream())
            {
                GifWriter gifWriter = new GifWriter(msGif, 50, -1);

                for (int i = 0; i < frames; i++)
                {
                    using (MemoryStream msImage = new MemoryStream())
                    {
                        mb.GetImage(minX, maxY, minY, maxY, k, power, startHue, endHue).Save(msImage, System.Drawing.Imaging.ImageFormat.Png);
                        gifWriter.WriteFrame(Image.FromStream(msImage));
                    }

                    float toAddminX = xMinParticle * (float)(frames - i);
                    float toAddmaxX = xMaxParticle * (float)(frames - i);
                    float toAddminY = yMinParticle * (float)(frames - i);
                    float toAddmaxY = yMaxParticle * (float)(frames - i);

                    minX += toAddminX;
                    minY += toAddminY;
                    maxX += toAddmaxX;
                    maxY += toAddmaxY;
                }

                File.WriteAllBytes("test.gif", msGif.GetBuffer());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float minX = float.Parse(textBox1.Text);
            float minY = float.Parse(textBox2.Text);

            float maxX = float.Parse(textBox3.Text);
            float maxY = float.Parse(textBox4.Text);

            int k = int.Parse(textBox11.Text);
            float power = float.Parse(textBox12.Text);

            int startHue = int.Parse(textBox13.Text);
            int endHue = int.Parse(textBox14.Text);

            MemoryStream ms = new MemoryStream();
            mb.GetImage(minX, maxY, minY, maxY, k, power, startHue, endHue).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.Image = Image.FromStream(ms);
        }
    }
}
