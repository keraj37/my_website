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

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        private Mandelbrot mb = new Mandelbrot();

        public Form1()
        {
            mb = new Mandelbrot();
            InitializeComponent();

            textBox1.Text = "-2";
            textBox2.Text = "-2";
            textBox3.Text = "2";
            textBox4.Text = "2";

            textBox8.Text = "-0.53";
            textBox7.Text = "-0.53";
            textBox6.Text = "-0.52";
            textBox5.Text = "-0.52";
            textBox9.Text = "100";
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

            float toAddminX = (tominX - minX) / (float)frames;
            float toAddminY = (tominY - minY) / (float)frames;
            float toAddmaxX = (tomaxX - maxX) / (float)frames;
            float toAddmaxY = (tomaxY - maxY) / (float)frames;

            textBox10.Text = "toAddminX: " + toAddminX + "toAddminY: " + toAddminY + "toAddmaxX: " + toAddmaxX + "toAddmaxY: " + toAddmaxY;

            for (int i = 0; i < frames; i++)
            {
                mb.GetImage(minX, maxY, minY, maxY).Save(i.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);

                minX += toAddminX;
                minY += toAddminY;
                maxX += toAddmaxX;
                maxY += toAddmaxY;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float minX = float.Parse(textBox1.Text);
            float minY = float.Parse(textBox2.Text);

            float maxX = float.Parse(textBox3.Text);
            float maxY = float.Parse(textBox4.Text);

            MemoryStream ms = new MemoryStream();
            mb.GetImage(minX, maxY, minY, maxY).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.Image = Image.FromStream(ms);
            ms.Dispose();
        }
    }
}
