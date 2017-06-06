using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Drawing.Imaging;
using my_website.Controllers.Console.Commands.Attributes;

namespace my_website.Controllers.MandelbrotSet.Solution01
{
    /* Original project: https://www.codeproject.com/Articles/1177443/Mandelbrot-Set-With-Csharp
     * "This program has been made by Joseph Dillon. Created between July 2016-March 2017"
     * Modified/ported by Jerry Switalski from WinForms to ASP.NET.
     */

    public class Mandelbrot
    {
        private const int DEFAULT_WIDTH = 700;
        private const int DEFAULT_HEIGHT = 700;
        private const int DEFAULT_ZOOM = 1;
        private const int DEFAULT_K = 50;
        private const int DEFAULT_STEP = 1;

        private ScreenPixelManage myPixelManager;
        private double yMin = -2.0;
        private double yMax = 2.0;
        private double xMin = -2.0;
        private double xMax = 2.0;
        private int kMax = 50;
        private int numColours = 1024;
        private int zoomScale = 7;

        private ColourTable colourTable = null;
        
        public byte[] GetImage(ConsoleCommandVariableAttribute.Vo[] objs)
        {
            int width = DEFAULT_WIDTH, height = DEFAULT_HEIGHT, zoomScaleParam = DEFAULT_ZOOM, kMaxParam = DEFAULT_K, xyPixelStepParam = DEFAULT_STEP;

            for (int i = 0; i < objs.Length; i++)
            {
                switch (i)
                {
                    case 0: width = objs[i].IntValue ?? DEFAULT_WIDTH; break;
                    case 1: height = objs[i].IntValue ?? DEFAULT_HEIGHT; break;
                    case 2: zoomScaleParam = objs[i].IntValue ?? DEFAULT_ZOOM; break;
                    case 3: kMaxParam = objs[i].IntValue ?? DEFAULT_K; break;
                    case 4: xyPixelStepParam = objs[i].IntValue ?? DEFAULT_STEP; break;
                    case 5: yMin = objs[i].FloatValue ?? yMin; break;
                    case 6: yMax = objs[i].FloatValue ?? yMax; break;
                    case 7: xMin = objs[i].FloatValue ?? xMin; break;
                    case 8: xMax = objs[i].FloatValue ?? xMax; break;
                }
            }

            Bitmap bmp = new Bitmap(width, height);
            MakeBlack(bmp);

            kMax = kMaxParam;
            numColours = kMax;

            if (colourTable == null)
            {
                colourTable = new ColourTable(numColours);
            }

            zoomScale = zoomScaleParam;

            int kLast = -1;
            double modulusSquared;
            Color color;
            Color colorLast = Color.Red;

            ComplexPoint screenBottomLeft = new ComplexPoint(xMin, yMin);
            ComplexPoint screenTopRight = new ComplexPoint(xMax, yMax);

            myPixelManager = new ScreenPixelManage(bmp, screenBottomLeft, screenTopRight);

            int xyPixelStep = xyPixelStepParam;
            ComplexPoint pixelStep = new ComplexPoint(xyPixelStep, xyPixelStep);
            ComplexPoint xyStep = myPixelManager.GetDeltaMathsCoord(pixelStep);

            int yPix = bmp.Height - 1;
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
                        zk = zk.DoCmplxSqPlusConst(c);
                        modulusSquared = zk.DoModulusSq();
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
                    }
                    xPix += xyPixelStep;
                }
                yPix -= xyPixelStep;
            }

            byte[] bytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bmp.Save(memoryStream, ImageFormat.Png);
                bytes = memoryStream.GetBuffer();
            }

            return bytes;
        }

        private static void MakeBlack(Bitmap bmp)
        {
            for(int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, Color.Black);
                }
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

        private class ColourTable
        {
            public int nColour;
            private Color[] colourTable;

            public ColourTable(int n)
            {
                nColour = n;
                colourTable = new Color[nColour];

                for (int i = 0; i < nColour; i++)
                {
                    double colourIndex = ((double)i) / (double)nColour;
                    double hue = Math.Pow(colourIndex, 0.15);

                    colourTable[i] = ColorFromHSLA(hue, 0.8, 0.52);
                }
            }

            public Color GetColour(int k)
            {
                return colourTable[k];
            }
        }
    }
}
