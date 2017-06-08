using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mandelbrot
{
    public class ScreenPixelManage
    {
        public int xPixel;
        public int yPixel;
        private double convConstX1;
        private double convConstX2;
        private double convConstY1;
        private double convConstY2;

        public class PixelCoord
        {
            public int xPixel;
            public int yPixel;
        }

        public ScreenPixelManage(Bitmap graphics, ComplexPoint screenBottomLeftCorner, ComplexPoint screenTopRightCorner)
        {
            convConstX1 = graphics.Width / (screenTopRightCorner.x - screenBottomLeftCorner.x);
            convConstX2 = convConstX1 * screenBottomLeftCorner.x;

            convConstY1 = graphics.Size.Height * (1.0 + screenBottomLeftCorner.y / (screenTopRightCorner.y - screenBottomLeftCorner.y));
            convConstY2 = graphics.Size.Height / (screenTopRightCorner.y - screenBottomLeftCorner.y);
        }

        public PixelCoord GetPixelCoord(ComplexPoint cmplxPoint)
        {
            PixelCoord result = new PixelCoord();
            result.xPixel = (int)(convConstX1 * cmplxPoint.x - convConstX2);
            result.yPixel = (int)(convConstY1 - convConstY2 * cmplxPoint.y);
            return result;
        }

        public ComplexPoint GetDeltaMathsCoord(ComplexPoint pixelCoord)
        {
            ComplexPoint result = new ComplexPoint(
                   pixelCoord.x / convConstX1,
                   pixelCoord.y / convConstY2);
            return result;
        }

        public ComplexPoint GetAbsoluteMathsCoord(ComplexPoint pixelCoord)
        {
            ComplexPoint result = new ComplexPoint(
                   (convConstX2 + pixelCoord.x) / convConstX1,
                   (convConstY1 - pixelCoord.y) / convConstY2);
            return result;
        }
    }
}
