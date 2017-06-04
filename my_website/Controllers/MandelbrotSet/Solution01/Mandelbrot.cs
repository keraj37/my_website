using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace my_website.Controllers.MandelbrotSet.Solution01
{
    /* Original project: https://www.codeproject.com/Articles/1177443/Mandelbrot-Set-With-Csharp
     * "This program has been made by Joseph Dillon.Created between July 2016-March 2017"
     * Modified/ported by Jerry Switalski from WinForms to ASP.NET.
     */

    public class Mandelbrot
    {
        private ScreenPixelManage myPixelManager;
        private ComplexPoint zoomCoord1 = new ComplexPoint(-1, 1);
        private ComplexPoint zoomCoord2 = new ComplexPoint(-2, 1);
        private double yMin = -2.0;
        private double yMax = 0.0;
        private double xMin = -2.0;
        private double xMax = 1.0;
        private int kMax = 50;
        private int numColours = 85;
        private int zoomScale = 7;

        private Graphics g;
        private Bitmap myBitmap;
        private double xValue;
        private double yValue;
        private int undoNum = 0;
        private string userName;
        private ColourTable colourTable = null;

        public void RenderImageToBitmap(Bitmap myBitmap, int? kMaxParam = 50, int? zoomScaleParam = 1, int? xyPixelStepParam = 1)
        {
            kMax = (int)kMaxParam;
            numColours = kMax;

            if ((colourTable == null) || (kMax != colourTable.kMax) || (numColours != colourTable.nColour))
            {
                colourTable = new ColourTable(numColours, kMax);
            }

            zoomScale = (int)zoomScaleParam;

            int kLast = -1;
            double modulusSquared;
            Color color;
            Color colorLast = Color.Red;

            ComplexPoint screenBottomLeft = new ComplexPoint(xMin, yMin);
            ComplexPoint screenTopRight = new ComplexPoint(xMax, yMax);

            myPixelManager = new ScreenPixelManage(myBitmap, screenBottomLeft, screenTopRight);

            int xyPixelStep = (int)xyPixelStepParam;
            ComplexPoint pixelStep = new ComplexPoint(xyPixelStep, xyPixelStep);
            ComplexPoint xyStep = myPixelManager.GetDeltaMathsCoord(pixelStep);

            int yPix = myBitmap.Height - 1;
            for (double y = yMin; y < yMax; y += xyStep.y)
            {
                int xPix = 0;
                for (double x = xMin; x < xMax; x += xyStep.x)
                {
                    ComplexPoint c = new ComplexPoint(x, y);
                    ComplexPoint zk = new ComplexPoint(0, 0);
                    int k = 0;
                    do
                    {
                        zk = zk.doCmplxSqPlusConst(c);
                        modulusSquared = zk.doMoulusSq();
                        k++;
                    }
                    while ((modulusSquared <= 4.0) && (k < kMax));

                    if (k < kMax)
                    {
                        if (k == kLast)
                        {
                            color = colorLast;
                        }
                        else
                        {
                            color = colourTable.GetColour(k);
                            colorLast = color;
                        }

                        if (xyPixelStep == 1)
                        {
                            if ((xPix < myBitmap.Width) && (yPix >= 0))
                            {
                                myBitmap.SetPixel(xPix, yPix, color);
                            }
                        }
                        else
                        {
                            for (int pX = 0; pX < xyPixelStep; pX++)
                            {
                                for (int pY = 0; pY < xyPixelStep; pY++)
                                {
                                    if (((xPix + pX) < myBitmap.Width) && ((yPix - pY) >= 0))
                                    {
                                        myBitmap.SetPixel(xPix + pX, yPix - pY, color);
                                    }
                                }
                            }
                        }
                    }
                    xPix += xyPixelStep;
                }
                yPix -= xyPixelStep;
            }
        }

        private static Color ColorFromHSLA(double H, double S, double L)
        {
            double v;
            double r, g, b;

            r = L;
            g = L;
            b = L;

            // Standard HSL to RGB conversion. This is described in
            // detail at:
            // http://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/
            v = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);

            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = L + L - v;
                sv = (v - m) / v;
                H *= 6.0;
                sextant = (int)H;
                fract = H - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;

                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;

                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;

                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;

                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;

                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            Color color = Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
            return color;
        }

        /*
        /// <summary>
        /// On-click handler for main form. Defines the points (lower-left and upper-right)
        /// of a zoom rectangle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseClickOnForm(object sender, MouseEventArgs e)
        {
            if (zoomCheckbox.Checked)
            {
                Pen box = new Pen(Color.Black);
                double x = Convert.ToDouble(e.X);
                xValue = x;
                double y = Convert.ToDouble(e.Y);
                yValue = y;

                try
                {
                    zoomScale = Convert.ToInt16(zoomTextBox.Text);
                }
                catch (Exception c)
                {
                    MessageBox.Show("Error: " + c.Message, "Error");
                }
                // Zoom scale has to be above 0, or their is no point in zooming.
                if (zoomScale < 1)
                {
                    MessageBox.Show("Zoom scale must be above 0");
                    zoomScale = 7;
                    zoomTextBox.Text = "7";
                    return;
                }

                ComplexPoint pixelCoord = new ComplexPoint((int)(xValue - (1005 / (zoomScale)) / 4), (int)(yValue - (691 / (zoomScale)) / 4));//
                zoomCoord1 = myPixelManager.GetAbsoluteMathsCoord(pixelCoord);
            }
        }

        /// <summary>
        /// Mouse-up handler for main form. The coordinates of the rectangle are
        /// saved so the new drawing can be rendered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseUpOnForm(object sender, MouseEventArgs e)
        {
            if (zoomCheckbox.Checked)
            {
                double x = Convert.ToDouble(e.X);
                double y = Convert.ToDouble(e.Y);

                ComplexPoint pixelCoord = new ComplexPoint((int)(xValue + (1005 / (zoomScale)) / 4), (int)(yValue + (691 / (zoomScale)) / 4));//
                zoomCoord2 = myPixelManager.GetAbsoluteMathsCoord(pixelCoord);

                // Swap to ensure that zoomCoord1 stores the lower-left
                // coordinate for the zoom region, and zoomCoord2 stores the
                // upper right coordinate.
                if (zoomCoord2.x < zoomCoord1.x)
                {
                    double temp = zoomCoord1.x;
                    zoomCoord1.x = zoomCoord2.x;
                    zoomCoord2.x = temp;
                }
                if (zoomCoord2.y < zoomCoord1.y)
                {
                    double temp = zoomCoord1.y;
                    zoomCoord1.y = zoomCoord2.y;
                    zoomCoord2.y = temp;
                }
                yMinCheckBox.Text = Convert.ToString(zoomCoord1.y);
                yMaxCheckBox.Text = Convert.ToString(zoomCoord2.y);
                xMinCheckBox.Text = Convert.ToString(zoomCoord1.x);
                xMaxCheckBox.Text = Convert.ToString(zoomCoord2.x);
                RenderImage();
            }
        }

        /// <summary>
        /// This will apply the zoom rectangle coordinates to the
        /// yMin yMax, xMin xMax text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            yMinCheckBox.Text = Convert.ToString(zoomCoord1.y);
            yMaxCheckBox.Text = Convert.ToString(zoomCoord2.y);
            xMinCheckBox.Text = Convert.ToString(zoomCoord1.x);
            xMaxCheckBox.Text = Convert.ToString(zoomCoord2.x);
        }
        */

        private class ColourTable
        {
            public int kMax;
            public int nColour;
            private double scale;
            private Color[] colourTable;

            public ColourTable(int n, int kMax)
            {
                nColour = n;
                this.kMax = kMax;
                scale = ((double)nColour) / kMax;
                colourTable = new Color[nColour];

                for (int i = 0; i < nColour; i++)
                {
                    double colourIndex = ((double)i) / nColour;
                    double hue = Math.Pow(colourIndex, 0.25);
                    colourTable[i] = ColorFromHSLA(hue, 0.9, 0.6);
                }
            }

            public Color GetColour(int k)
            {
                return colourTable[k];
            }
        }
    }
}
