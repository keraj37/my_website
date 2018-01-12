using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Drawing.Imaging;

namespace Mandelbrot
{
    /* Original project: https://www.codeproject.com/Articles/1177443/Mandelbrot-Set-With-Csharp
     * "This program has been made by Joseph Dillon. Created between July 2016-March 2017"
     * Modified/ported by J.S. from WinForms to ASP.NET.
     */

    public class Mandelbrot
    {
        public Bitmap GetImage(int width, int height, double xMinParam, double xMaxParam, double yMinParam, double yMaxParam, int kParam, double power, int startHue, int endHue, double power2, float light)
        {
            int kMax = kParam;
            int xyPixelStep = 1;
            double yMin = yMinParam;
            double yMax = yMaxParam;
            double xMin = xMinParam;
            double xMax = xMaxParam;

            Bitmap bmp = new Bitmap(width, height);

            int numColours = kMax;

            ColourTable colourTable = new ColourTable(numColours, power, power2, startHue, endHue, light);

            int kLast = -1;
            double modulusSquared;
            Color color;
            Color colorLast = Color.Red;

            ComplexPoint screenBottomLeft = new ComplexPoint(xMinParam, yMinParam);
            ComplexPoint screenTopRight = new ComplexPoint(xMaxParam, yMaxParam);

            ScreenPixelManage myPixelManager = new ScreenPixelManage(bmp, screenBottomLeft, screenTopRight);
            
            ComplexPoint pixelStep = new ComplexPoint(xyPixelStep, xyPixelStep);
            ComplexPoint xyStep = myPixelManager.GetDeltaMathsCoord(pixelStep);

            int yPix = bmp.Height - 1;
            for (double y = yMinParam; y < yMaxParam; y += xyStep.y)
            {
                int xPix = 0;
                for (double x = xMinParam; x < xMaxParam; x += xyStep.x)
                {
                    ComplexPoint c = new ComplexPoint(x, y);
                    ComplexPoint zk = new ComplexPoint(0, 0);
                    int k = 0;
                    do
                    {
                        zk = zk.DoCmplxSqPlusConst(c);
                        modulusSquared = zk.DoModulusSq();
                        k++;
                    }
                    while ((modulusSquared <= 4.0) && (k < kMax));

                    if (k == kLast)
                    {
                        color = colorLast;
                    }
                    else
                    {
                        color = colourTable.GetColour(k);
                        colorLast = color;
                    }

                    if (k == kMax)
                    {
                        color = Color.Black;
                    }

                    if (xyPixelStep == 1)
                    {
                        if ((xPix < bmp.Width) && (yPix >= 0))
                        {
                            bmp.SetPixel(xPix, yPix, color);
                        }
                    }
                    else
                    {
                        for (int pX = 0; pX < xyPixelStep; pX++)
                        {
                            for (int pY = 0; pY < xyPixelStep; pY++)
                            {
                                if (((xPix + pX) < bmp.Width) && ((yPix - pY) >= 0))
                                {
                                    bmp.SetPixel(xPix + pX, yPix - pY, color);
                                }
                            }
                        }
                    }

                    xPix += xyPixelStep;
                }
                yPix -= xyPixelStep;
            }

            return bmp;
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

        private class ColourTable
        {
            public int nColour;
            private Color[] colourTable;

            public ColourTable(int n, double colpow, double colpow2, double startHue, double endHue, float light)
            {
                nColour = n;
                colourTable = new Color[nColour + 1];

                for (int i = 0; i <= nColour; i++)
                {
                    double diff = endHue - startHue;
                    double perc = diff / 360d; 

                    double colourIndex = (((double)i) / (double)nColour);
                    double hue = colourIndex * perc;

                    double x = (double)i / (double)nColour;
                    double y = Math.Pow(x, colpow);

                    hue *= y;

                    colourTable[nColour - i] = ColorFromHSLA((startHue / 360d) + hue, light, -(Math.Pow(x, colpow2)) + 1f);
                }
            }

            public Color GetColour(int k)
            {
                return colourTable[k];
            }
        }
    }
}
